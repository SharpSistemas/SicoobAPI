/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.PIX;

using CS.BCB.PIX.Contratos;
using CS.BCB.PIX.Models;
using Sicoob.Shared;
using Sicoob.Shared.Models.Acesso;
using Simple.API;
using System;
using System.Globalization;
using System.Threading.Tasks;

/// <summary>
/// Classe para comunicação com as APIs de PIX do Sicoob
/// </summary>
public sealed class SicoobPIX : Shared.Sicoob, IApiPix
{
    // Documentações
    // > APIs tipo "Swagger":
    //   https://developers.sicoob.com.br/#!/apis
    // > APIs no POSTMAN:
    //   https://documenter.getpostman.com/view/20565799/UzBnrmod#239d9f68-d646-4209-994a-cd564b6d6d1a
    // Auxiliares
    // > Parseia BR Code para debugar:
    //   https://openpix.com.br/qrcode/scanner/
    // > Gera QR para teste:
    //   https://webqr.com/create.html

    private ClientInfo clientApi;
    public Shared.Models.ConfiguracaoAPI ConfigApi { get; }

    public SicoobPIX(Shared.Models.ConfiguracaoAPI configApi, System.Security.Cryptography.X509Certificates.X509Certificate2? certificado = null)
        : base(configApi, certificado)
    {
        ConfigApi = configApi;
    }

    protected override void setupClients(System.Net.Http.HttpClientHandler handler)
    {
        clientApi = new ClientInfo(ConfigApi.UrlApi, handler);
        clientApi.SetHeader("x-sicoob-clientid", ConfigApi.ClientId);

#if DEBUG
        enableDebug(clientApi);
#endif

    }
    protected override void atualizaClients(TokenResponse token)
    {
        clientApi.SetAuthorizationBearer(token.access_token);
    }

    /* COB */
    /// <summary>
    /// Endpoint que cria uma cobrança imediata (QRCode Pix).
    /// </summary>
    /// <param name="transactionId">String, deve ter de 27 a 36 caracteres. Identificador único da cobrança Pix</param>
    /// <param name="cobranca">Dados para geração da cobrança imediata.</param>
    /// <returns>Cobrança imediata criada</returns>
    public async Task<CobrancaImediata> CriarCobrancaAsync(string transactionId, NovaCobrancaImediata cobranca)
    {
        validaTxID(transactionId);
        return await ExecutaChamadaAsync(() => clientApi.PutAsync<CobrancaImediata>($"/pix/api/v2/cob/{transactionId}", cobranca));
    }
    /// <summary>
    /// Endpoint para criar uma cobrança imediata, neste caso, o txid deve ser definido pelo PSP.
    /// </summary>
    /// <param name="cobranca">Dados para geração da cobrança imediata.</param>
    /// <returns>Cobrança imediata criada</returns>
    public async Task<CobrancaImediata> CriarCobrancaAsync(NovaCobrancaImediata cobranca)
        => await ExecutaChamadaAsync(() => clientApi.PostAsync<CobrancaImediata>($"/pix/api/v2/cob", cobranca));
    /// <summary>
    /// Endpoint para revisar uma cobrança através de um determinado txid.
    /// </summary>
    /// <param name="transactionId">String, deve ter de 27 a 36 caracteres. Identificador único da cobrança Pix.</param>
    /// <param name="cobranca">Dados para geração da cobrança</param>
    /// <returns>Cobrança imediata revisada. A revisão deve ser incrementada em 1.</returns>
    public async Task<CobrancaImediata> RevisarCobrancaAsync(string transactionId, RevisarCobrancaImediata cobranca)
    {
        validaTxID(transactionId);
        return await ExecutaChamadaAsync(() => clientApi.PatchAsync<CobrancaImediata>($"/pix/api/v2/cob/{transactionId}", cobranca));
    }

    /// <summary>
    /// Endpoint para consultar uma cobrança através de um determinado txid.
    /// </summary>
    /// <param name="transactionId">String, deve ter de 27 a 36 caracteres. Identificador único da cobrança Pix.</param>
    /// <param name="revisao">Revisao a ser consultada</param>
    /// <returns>Dados da cobrança imediata</returns>
    public async Task<CobrancaImediata> ConsultarCobrancaAsync(string transactionId, int? revisao = null)
    {
        validaTxID(transactionId);

        string url = $"/pix/api/v2/cob/{transactionId}";
        if (revisao.HasValue) url += $"?revisao={revisao.Value}";

        return await ExecutaChamadaAsync(() => clientApi.GetAsync<CobrancaImediata>(url));
    }
    /// <summary>
    /// Endpoint para consultar cobranças imediatas através de parâmetros como início, fim, cpf, cnpj e status.
    /// </summary>
    /// <param name="consulta">Dados da consulta</param>
    /// <returns>Lista de cobranças imediatas.</returns>
    public async Task<ListagemCobrancaImediata> ListarCobrancasAsync(ConsultarCobrancaImediata consulta)
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<ListagemCobrancaImediata>("/pix/api/v2/cob", consulta.ToKVP()));

    /// <summary>
    /// Endpoint para gerar a imagem qrcode de uma cobrança através de um determinado txid.
    /// </summary>
    /// <param name="transactionId">String, deve ter de 27 a 36 caracteres. Identificador único da cobrança Pix.</param>
    /// <param name="revisao">Revisao a ser consultada</param>
    /// <param name="largura">Largura da imagem a ser exibida</param>
    /// <returns>Bytes da imagem codificada em PNG</returns>
    [Obsolete("Sicoob removeu o endpoint", true)]
    public async Task<byte[]> ConsultarImagemCobrancaAsync(string transactionId, int? revisao = null, int? largura = null)
    {
        validaTxID(transactionId);
        string url = $"/pix/api/v2/cob/{transactionId}/imagem";

        return await ExecutaChamadaAsync(() => clientApi.GetAsync<byte[]>(url, new { revisao, largura }.ToKVP()));
    }

    /* COBV */

    /* COBV-Lote */

    /* PayloadLocation */

    /* PIX */
    /// <summary>
    /// Endpoint para consultar Pix recebidos
    /// </summary>
    /// <param name="consulta">Dados da consulta</param>
    /// <returns>Lista dos Pix recebidos de acordo com o critério de busca.</returns>
    public async Task<ListagemPixRecebido> ListarPIXAsync(ConsultarPix consulta)
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<ListagemPixRecebido>("/pix/api/v2/pix", consulta.ToKVP()));
    /// <summary>
    /// Endpoint para consultar um Pix através de um e2eid.
    /// </summary>
    /// <param name="endToEndId">Id fim a fim da transação. Deve ter 32 caracteres.</param>
    /// <returns>Dados do Pix efetuado.</returns>
    public async Task<PixRecebido> ConsultarPIXAsync(string endToEndId)
         => await ExecutaChamadaAsync(() => clientApi.GetAsync<PixRecebido>($"/pix/api/v2/pix/{endToEndId}"));

    /// <summary>
    /// Endpoint para solicitar uma devolução através de um e2eid do Pix e do ID da devolução.
    /// O motivo que será atribuído à PACS.004 será "Devolução solicitada pelo usuário recebedor do pagamento original" 
    /// cuja sigla é "MD06" de acordo com a aba RTReason da PACS.004 que consta no Catálogo de Mensagens do Pix.
    /// </summary>
    /// <param name="endToEndId">Id fim a fim da transação.</param>
    /// <param name="idDevolucao">Id gerado pelo cliente para representar unicamente uma devolução.</param>
    /// <param name="valor">Valor a ser devolvido</param>
    /// <returns>Dados da devolução</returns>
    public async Task<PixDevolucao> SolicitarDevlucaoPixAsync(string endToEndId, string idDevolucao, decimal valor)
    {
        validaIDDevolucao(idDevolucao);
        string url = $"/pix/api/v2/pix/{endToEndId}/devolucao/{idDevolucao}";
        return await ExecutaChamadaAsync(() => clientApi.PutAsync<PixDevolucao>(url, new { valor = valor.ToString("N2", CultureInfo.InvariantCulture) }));
    }
    /// <summary>
    /// Endpoint para consultar uma devolução através de um EndToEndID do Pix e do ID da devolução
    /// </summary>
    /// <param name="endToEndId">Id fim a fim da transação.</param>
    /// <param name="idDevolucao">Id gerado pelo cliente para representar unicamente uma devolução.</param>
    /// <returns>Dados da devolução</returns>
    public async Task<PixDevolucao> ConsultarDevlucaoPixAsync(string endToEndId, string idDevolucao)
    {
        string url = $"/pix/api/v2/pix/{endToEndId}/devolucao/{idDevolucao}";
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<PixDevolucao>(url));
    }

    /* Webhook */
    /// <summary>
    /// Endpoint para configuração do serviço de notificações acerca de Pix recebidos. 
    /// Somente Pix associados a um txid serão notificados.
    /// </summary>
    /// <param name="chave">Chave a ser associada</param>
    /// <param name="url">Url a ser chamada com POST. Será concatenado `/pix` ao final.</param>
    public async Task CriarWebHookAsync(string chave, string url)
    {
        await ExecutaChamadaAsync(() => clientApi.PutAsync($"/pix/api/v2/webhook/{chave}", new { webhookUrl = url }));
    }
    /// <summary>
    /// Endpoint para consultar Webhooks cadastrados
    /// </summary>
    public async Task<ListagemWebhookAtivo> ConsultarWebHooksAsync()
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<ListagemWebhookAtivo>("/pix/api/v2/webhook"));
    /// <summary>
    /// Endpoint para recuperação de informações sobre o Webhook Pix.
    /// </summary>
    public async Task<WebhookAtivo> ConsultarWebHookAsync(string chave)
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<WebhookAtivo>($"/pix/api/v2/webhook/{chave}"));
    /// <summary>
    /// Endpoint para cancelamento do webhook. Não é a única forma pela qual um webhook pode ser removido.
    /// </summary>
    public async Task CancelarWebHookAsync(string chave)
        => await ExecutaChamadaAsync(() => clientApi.DeleteAsync($"/pix/api/v2/webhook/{chave}"));

    /* Validação de IDs */
    private static void validaTxID(string transactionId)
    {
        if (string.IsNullOrEmpty(transactionId))
        {
            throw new ArgumentException($"'{nameof(transactionId)}' cannot be null or empty.", nameof(transactionId));
        }
        if (!CS.BCB.PIX.Validadores.ValidacaoIdentificadores.ValidaTransactionId(transactionId))
        {
            throw new ArgumentException($"'{nameof(transactionId)}' Não é valido na restrição", nameof(transactionId));
        }
    }
    private static void validaIDDevolucao(string idDevolucao)
    {
        if (string.IsNullOrEmpty(idDevolucao))
        {
            throw new ArgumentException($"'{nameof(idDevolucao)}' cannot be null or empty.", nameof(idDevolucao));
        }

        if (!CS.BCB.PIX.Validadores.ValidacaoIdentificadores.ValidaIdDevolucao(idDevolucao))
        {
            throw new ArgumentException($"'{nameof(idDevolucao)}' Não é valido na restrição", nameof(idDevolucao));
        }
    }

    /* Não implementados */
    public Task<CobrancaVencimento> ConsultarCobrancaVencimentoAsync(string transactionId, int? revisao = null)
    {
        throw new NotImplementedException();
    }
    public Task<CobrancaVencimento> CriarCobrancaVencimentoAsync(string transactionId, NovaCobrancaVencimento cobranca)
    {
        throw new NotImplementedException();
    }
    public Task<ListagemCobrancaVencimento> ListarCobrancasVencimentoAsync(ConsultarCobrancaImediata consulta)
    {
        throw new NotImplementedException();
    }
    public Task<CobrancaVencimento> RevisarCobrancaVencimentoAsync(string transactionId, RevisarCobrancaVencimento cobranca)
    {
        throw new NotImplementedException();
    }
    public Task CriarLoteCobrancaVencimentoAsync(string idLote, NovaCobrancaVencimentoLote lote)
    {
        throw new NotImplementedException();
    }
    public Task RevisarLoteCobrancaVencimentoAsync(string idLote, RevisarCobrancaVencimentoLote lote)
    {
        throw new NotImplementedException();
    }
    public Task<ConsultarCobrancaVencimentoLote> ConsultarLoteCobrancaVencimentoAsync(string transactionId)
    {
        throw new NotImplementedException();
    }
    public Task<ConsultaLotesCobranca> ListarLoteCobrancaVencimentoAsync(Consulta consulta)
    {
        throw new NotImplementedException();
    }
}
