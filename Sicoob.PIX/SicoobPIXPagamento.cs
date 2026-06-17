/**************************************\
 * Biblioteca C# para APIs do SICOOB    *
 * Autor:Lucas Moraes                   *
 * github.com/lucasmoraes804/SicoobAPI  *
\**************************************/

using Sicoob.Shared.Models;

namespace Sicoob.PIX;

using Sicoob.PIX.Models;
using Sicoob.Shared;
using Sicoob.Shared.Models.Acesso;
using Simple.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Classe para comunicacao com a API de Pix Pagamentos do Sicoob
/// </summary>
public sealed class SicoobPIXPagamento : Sicoob
{
    // Documentacoes
    // > APIs tipo "Swagger":
    //   https://developers.sicoob.com.br/#!/apis
    // > APIs no POSTMAN:
    //   https://d1zet1fng6kfaw.cloudfront.net/postman-collections/91d4408f020070992311e808993c5cd2-pix-pagamentos.postman_collection

    private ClientInfo clientApi;
    public Shared.Models.ConfiguracaoAPI ConfigApi { get; }
    public event Action<ConfiguracaoToken>? UpdateTokenEvent;

    public SicoobPIXPagamento(Shared.Models.ConfiguracaoAPI configApi, System.Security.Cryptography.X509Certificates.X509Certificate2? certificado = null)
        : base(configApi, certificado)
    {
        ConfigApi = configApi;
    }

    protected override void setupClients(HttpClientHandler handler)
    {
        clientApi = new ClientInfo(ConfigApi.UrlApi, handler);
        clientApi.SetHeader("client_id", ConfigApi.ClientId);
        
        if (ConfigApi.Token is not null)
            clientApi.SetAuthorizationBearer(ConfigApi.Token.Token);

#if DEBUG
        enableDebug(clientApi);
#endif
    }
    protected override void atualizaClients(TokenResponse token)
    {
        ConfigApi.Token = new Shared.Models.ConfiguracaoToken()
        {
            ExpiresAtUTC = DateTime.UtcNow.AddSeconds(token.expires_in),
            Token = token.access_token
        };
        //Notificar quando o token foi atualizado
        if (UpdateTokenEvent is not null)
            UpdateTokenEvent(ConfigApi.Token);
        
        clientApi.SetAuthorizationBearer(token.access_token);
    }

    /// <summary>
    /// Endpoint para consultar um pagamento Pix atraves de um e2eid.
    /// </summary>
    public async Task<PixPagamento> ConsultarPagamentoAsync(string endToEndId)
    {
        validaEndToEndId(endToEndId);
        return await ExecutaChamadaAsyncPIXPagamento(() => clientApi.GetAsync<PixPagamento>($"/pix-pagamentos/v2/pagamentos/{endToEndId}"));
    }

    /// <summary>
    /// Endpoint para iniciar um pagamento Pix por meio de chave DICT.
    /// </summary>
    public async Task<IniciarPagamentoResponse> IniciarPagamentoPorChaveAsync(IniciarPagamentoRequest pagamento)
    {
        if (pagamento is null)
        {
            throw new ArgumentNullException(nameof(pagamento));
        }
        if (string.IsNullOrWhiteSpace(pagamento.Chave))
        {
            throw new ArgumentException($"'{nameof(pagamento.Chave)}' não pode ser null ou vazio.", nameof(pagamento));
        }

        return await ExecutaChamadaAsyncPIXPagamento(() => clientApi.PostAsync<IniciarPagamentoResponse>("/pix-pagamentos/v2/pagamentos", pagamento));
    }

    /// <summary>
    /// Endpoint para efetivar um pagamento iniciado pela API de transferencia.
    /// </summary>
    public async Task<PixPagamento> EfetivarPagamentoAsync(ConfirmarPagamentoRequest pagamento)
    {
        if (pagamento is null)
        {
            throw new ArgumentNullException(nameof(pagamento));
        }
        if (string.IsNullOrWhiteSpace(pagamento.EndToEndId))
        {
            throw new ArgumentException($"'{nameof(pagamento.EndToEndId)}' não pode ser null ou vazio.", nameof(pagamento));
        }
        validaEndToEndId(pagamento.EndToEndId);
        if (string.IsNullOrWhiteSpace(pagamento.Valor))
        {
            throw new ArgumentException($"'{nameof(pagamento.Valor)}' não pode ser null ou vazio.", nameof(pagamento));
        }

        return await ExecutaChamadaAsyncPIXPagamento(() => clientApi.PostAsync<PixPagamento>("/pix-pagamentos/v2/pagamentos/confirmacao", pagamento));
    }

    /// <summary>
    /// Endpoint para configuracao do webhook de pagamentos Pix.
    /// </summary>
    public async Task CriarWebHookAsync(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException($"'{nameof(url)}' não pode ser null ou vazio.", nameof(url));
        }

        await ExecutaChamadaAsyncPIXPagamento(() => clientApi.PutAsync("/pix-pagamentos/v2/pagamentos/webhook", new WebhookPagamentoRequest { WebhookUrl = url }));
    }
    /// <summary>
    /// Endpoint para consulta das informacoes do webhook de pagamentos Pix.
    /// </summary>
    public async Task<WebhookPagamento> ConsultarWebHookAsync()
        => await ExecutaChamadaAsyncPIXPagamento(() => clientApi.GetAsync<WebhookPagamento>("/pix-pagamentos/v2/pagamentos/webhook"));
    /// <summary>
    /// Endpoint para cancelamento do webhook de pagamentos Pix.
    /// </summary>
    public async Task CancelarWebHookAsync()
        => await ExecutaChamadaAsyncPIXPagamento(() => clientApi.DeleteAsync("/pix-pagamentos/v2/pagamentos/webhook"));

    private static void validaEndToEndId(string endToEndId)
    {
        if (string.IsNullOrEmpty(endToEndId))
        {
            throw new ArgumentException($"'{nameof(endToEndId)}' não pode ser null ou vazio.", nameof(endToEndId));
        }
        if (!CS.BCB.PIX.Validadores.ValidacaoIdentificadores.ValidaIdE2E(endToEndId))
        {
            throw new ArgumentException($"'{nameof(endToEndId)}' não é válido", nameof(endToEndId));
        }
    }

    private async Task<T> ExecutaChamadaAsyncPIXPagamento<T>(Func<Task<Response<T>>> func)
    {
        await VerificaAtualizaCredenciaisAsync();
        Response<T> response = await func();

        if (!response.IsSuccessStatusCode)
        {
            if (response.TryParseErrorResponseData(out CS.BCB.PIX.Models.ErroRequisicao err))
            {
                throw new CS.BCB.PIX.Excecoes.ErroRequisicaoException(err);
            }
        }
        response.EnsureSuccessStatusCode();

        return response.Data;
    }
    private async Task ExecutaChamadaAsyncPIXPagamento(Func<Task<Response>> func)
    {
        await VerificaAtualizaCredenciaisAsync();
        Response response = await func();

        if (response.IsSuccessStatusCode) return;
        if (response.TryParseErrorResponseData(out CS.BCB.PIX.Models.ErroRequisicao err))
        {
            throw new CS.BCB.PIX.Excecoes.ErroRequisicaoException(err);
        }
        response.EnsureSuccessStatusCode();
    }
}
