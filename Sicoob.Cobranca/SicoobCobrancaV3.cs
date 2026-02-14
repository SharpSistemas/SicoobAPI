/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Cobranca;

using Sicoob.Cobranca.Models.Shared;
using Sicoob.Cobranca.Models.v3;
using Sicoob.Shared.Models;
using Sicoob.Shared.Models.Acesso;
using Sicoob.Shared.Models.Geral;
using Simple.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Classe para comunicação com as APIs de Cobrança do Sicoob
/// </summary>
public sealed class SicoobCobrancaV3 : Shared.Sicoob
{
    // Documentações
    // > APIs tipo "Swagger":
    //   https://developers.sicoob.com.br/#!/apis
    // > Link que o Suporte do Sicoob enviou
    // https://documenter.getpostman.com/view/20565799/Uzs6yNhe#6447c293-f67b-44ba-b7be-41f5c3de978d

    private readonly int numeroContrato;
    private ClientInfo clientApi;
    private ConfiguracaoAPI ConfigApi { get; }
    public string? PastaCopiaMovimentacoes { get; set; }
    public delegate void UpdateToken(ConfiguracaoToken token);
    public event UpdateToken? UpdateTokenEvent;

    public SicoobCobrancaV3(ConfiguracaoAPI configApi, int nroContrato, System.Security.Cryptography.X509Certificates.X509Certificate2? certificado = null)
       : base(configApi, certificado)
    {
        numeroContrato = nroContrato;
        ConfigApi = configApi;
        clientApi = new ClientInfo(ConfigApi.UrlApi);
    }

    protected override void setupClients(HttpClientHandler handler)
    {
        clientApi = new ClientInfo(ConfigApi.UrlApi, handler);
        clientApi.SetHeader("client_id", ConfigApi.ClientId);

        if (ConfigApi.Token is not null)
            clientApi.SetAuthorizationBearer(ConfigApi.Token.Token);
    }
    protected override void atualizaClients(TokenResponse token)
    {
        ConfigApi.Token = new ConfiguracaoToken()
        {
            ExpiresAtUTC = DateTime.UtcNow.AddSeconds(token.expires_in),
            Token = token.access_token
        };
        //Notificar quando o token foi atualizado
        if (UpdateTokenEvent is not null)
            UpdateTokenEvent(ConfigApi.Token); 
        
        clientApi.SetAuthorizationBearer(token.access_token);
    }

    /* Boletos */

    /// <summary>
    /// Consulta boleto utilizando um dos três métodos de busca
    /// </summary>
    /// <param name="modalidade"><see cref="Modalidade"/></param>
    /// <param name="nossoNumero">Número identificador do boleto no Sisbr. Caso seja infomado, não é necessário infomar a linha digitável ou código de barras</param>
    /// <param name="linhaDigitavel">Número da linha digitável do boleto com 47 posições. Caso seja informado, não é necessário informar o nosso número ou código de barras</param>
    /// <param name="codigoBarras">Número de código de barras do boleto com 44 posições.Caso seja informado, não é necessário informar o nosso número ou linha digitável</param>
    /// <returns>Boleto buscado</returns>
    public async Task<ConsultaBoletoResponse?> ConsultarBoleto(int? nossoNumero = null, string? linhaDigitavel = null, string? codigoBarras = null, int modalidade = (int)Modalidade.SimplesComRegistro)
    {
        var consulta = new ConsultaBoletoRequest()
        {
            codigoModalidade = modalidade,
            numeroCliente = numeroContrato,
            nossoNumero = nossoNumero,
            linhaDigitavel = linhaDigitavel,
            codigoBarras = codigoBarras
        };
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaBoletoResponse?>(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos", consulta));
    }

    /// <summary>
    /// Consulta boletos de um Pagador
    /// </summary>
    /// <param name="numeroCpfCnpj">CPF/CNPJ do Pagador</param>
    /// <param name="codigoSituacao">Código da Situação do Boleto. 1: Em aberto, 2: Baixado, 3: Liquidado</param>
    /// <param name="dataVencimentoInicio">Data de Vencimento Inicial</param>
    /// <param name="dataVencimentoFim">Data de Vencimento Final</param>
    /// <returns>Boletos do Pagador</returns>
    public async Task<ConsultaBoletosPagadorResponse> ConsultarBoletosPagador(string numeroCpfCnpj, int? codigoSituacao = null, DateTime? dataVencimentoInicio = null, DateTime? dataVencimentoFim = null)
    {
        var consulta = new ConsultaBoletoPagadorRequest()
        {
            numeroCliente = numeroContrato,
            codigoSituação = codigoSituacao,
            dataInicio = dataVencimentoInicio?.ToString("yyyy-MM-dd"),
            dataFim = dataVencimentoFim?.ToString("yyyy-MM-dd")
        };
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaBoletosPagadorResponse>(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos/pagadores/" + numeroCpfCnpj, consulta));
    }

    public async Task<ConsultaBoletoResponse?> ConsultarSegundaViaBoleto(int modalidade, int? nossoNumero = null, string? linhaDigitavel = null, string? codigoBarras = null, bool gerarPdf = false)
    {
        var consulta = new ConsultaBoletoRequest()
        {
            codigoModalidade = modalidade,
            numeroCliente = numeroContrato,
            nossoNumero = nossoNumero,
            linhaDigitavel = linhaDigitavel,
            codigoBarras = codigoBarras,
            gerarPdf = gerarPdf
        };
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaBoletoResponse?>(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos/segunda-via", consulta));
    }

    public async Task<IncluirBoletosResponse?> IncluirBoletos(IncluirBoletoRequest boleto)
    {
        return await ExecutaChamadaAsync(() => clientApi.PostAsync<IncluirBoletosResponse?>(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos", boleto));
    }

    /// <summary>
    /// Consulta de dados de faixas de nosso número disponíveis.
    /// Serviço para consulta de dados de faixas de nosso número disponíveis.
    /// Quando o campo validaDigitoVerificadorNossoNumero retornar o valor "0" a faixa "numeroInicial" e "numeroFinal" refere-se a numeração final (exemplo: 10 e 15 - utilização: 1-0 1-1 1-2 1-3 1-4 1-5).
    /// Mas se o campo validaDigitoVerificadorNossoNumero retornar o valor "1" a faixa "numeroInicial" e "numeroFinal" deverá ser calculado o DV (exemplo: 10 e 15 - utilização: 10-4 11-8 12-0 13-1 14-7 15-9).
    /// </summary>
    /// <param name="quantidade"></param>
    /// <param name="modalidade"></param>
    /// <param name="numeroContratoCobranca"></param>
    /// <returns></returns>
    public async Task<ConsultaBoletoResponse?> ConsultarFaixasNossoNumeroDisponivel(int quantidade, int modalidade = (int)Modalidade.SimplesComRegistro, int? numeroContratoCobranca = null)
    {
        var consulta = new ConsultaFaixasNossoNumeroRequest()
        {
            codigoModalidade = modalidade,
            numeroCliente = numeroContrato,
            quantidade = quantidade,
            numeroContratoCobranca = numeroContratoCobranca
        };
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaBoletoResponse?>(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos/faixas-nosso-numero", consulta));
    }
    
    public async Task BaixarBoletos(int nossoNumero, int codigoModalidade)
    {
        var baixa = new BaixarBoletoRequest
        {
            numeroCliente = numeroContrato,
            codigoModalidade = codigoModalidade
        };
        await ExecutaChamadaAsync(() => clientApi.PostAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/boletos/{nossoNumero}/baixar", baixa));
    }

    public async Task AlterarBoleto(int nossoNumero, AlterarBoletoRequest boletos)
    {
        await ExecutaChamadaAsync(() => clientApi.PatchAsync(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos/" + nossoNumero, boletos));
    }

    /* Pagador */
    public async Task PagadorBoletos(DadosPagadorRequest dadosPagador)
        => await ExecutaChamadaAsync(() => clientApi.PutAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/pagadores", dadosPagador));

    /* Negativação */
    public async Task NegativarBoletos(int nossoNumero, int codigoModalidade)
    {
        var protesto = new ProtestoRequest
        {
            numeroCliente = numeroContrato,
            codigoModalidade = codigoModalidade,
        };
        await ExecutaChamadaAsync(() => clientApi.PostAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/boletos/{nossoNumero}/negativacoes", protesto));
    }
    
    public async Task CancelarNegativacaoBoletos(int nossoNumero, int codigoModalidade)
    {
        var protesto = new ProtestoRequest
        {
            numeroCliente = numeroContrato,
            codigoModalidade = codigoModalidade,
        };
        await ExecutaChamadaAsync(() => clientApi.PatchAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/boletos/{nossoNumero}/negativacoes", protesto));
    }

    public async Task BaixarNegativacaoBoletos(int nossoNumero)
     => await ExecutaChamadaAsync(() => clientApi.DeleteAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/boletos/{nossoNumero}/negativacoes"));

    /* Protesto */
    public async Task ProtestarBoletos(int nossoNumero, int codigoModalidade)
    {
        var protesto = new ProtestoRequest
        {
            numeroCliente = numeroContrato,
            codigoModalidade = codigoModalidade,
        };
        await ExecutaChamadaAsync(() => clientApi.PostAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/boletos/{nossoNumero}/protestos", protesto));
    }

    public async Task CancelarProtestoBoletos(int nossoNumero, int codigoModalidade)
    {
        var protesto = new ProtestoRequest
        {
            numeroCliente = numeroContrato,
            codigoModalidade = codigoModalidade,
        };
        await ExecutaChamadaAsync(() => clientApi.PatchAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/boletos/{nossoNumero}/protestos", protesto));
    }

    public async Task DesistirProtestoBoletos(int nossoNumero)
     => await ExecutaChamadaAsync(() => clientApi.DeleteAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/boletos/{nossoNumero}/protestos"));
     
    /* Movimentação */
    public async Task<RetornoSolicitacaoMovimentacoesCarteira> SolicitarMovimentacao(Tipo tipoMovimento, DateTime data)
    {
        var di = data.Date;
        var df = data.Date.AddDays(1).AddSeconds(-1);
        return await SolicitarMovimentacao(tipoMovimento, di, df);
    }
    public async Task<RetornoSolicitacaoMovimentacoesCarteira> SolicitarMovimentacao(Tipo tipoMovimento, DateTime dataInicial, DateTime dataFinal)
        => await SolicitarMovimentacao(new MovimentacaoRequest() { numeroCliente = numeroContrato, tipoMovimento = (int)tipoMovimento, dataInicial = dataInicial, dataFinal = dataFinal });
    private async Task<RetornoSolicitacaoMovimentacoesCarteira> SolicitarMovimentacao(MovimentacaoRequest movimentacao)
    {
        var retorno = await ExecutaChamadaAsync(() => clientApi.PostAsync<ResponseMovimentacao<RetornoSolicitacaoMovimentacoesCarteira>>(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos/movimentacoes", movimentacao));
        return retorno.resultado;
    }
    public async Task<RetornoConsultaMovimentacoes?> ConsultarSituacaoSolicitacao(int codigoSolicitacao)
    {
        await VerificaAtualizaCredenciaisAsync();
        var result = await clientApi.GetAsync<ResponseMovimentacao<RetornoConsultaMovimentacoes>>(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos/movimentacoes", new { numeroCliente = numeroContrato, codigoSolicitacao });

        if (result.IsSuccessStatusCode) return result.Data.resultado;

        // "{\"mensagens\":[{\"mensagem\":\"Solicitação ainda em processamento.\",\"codigo\":\"5004\"}]}"
        if (result.TryParseErrorResponseData(out ErroRequisicao err))
        {
            if (err.mensagens == null) { }
            
            if (err.mensagens.Any(o => o.codigo == 5004)) return null;

            throw new ErroRequisicaoException(err);
        }
        result.EnsureSuccessStatusCode(); // Erro comum
        return null; // a linha de cima vai arremessar o erro padrão
    }

    private async Task<RetornoArquivoMovimentacao> DownloadArquivoMovimentacao(int codigoSolicitacao, int idArquivo)
    {
        var retorno = await ExecutaChamadaAsync(() => clientApi.GetAsync<ResponseMovimentacao<RetornoArquivoMovimentacao>>(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos/movimentacoes/download", new { numeroCliente = numeroContrato, codigoSolicitacao, idArquivo }));
        return retorno.resultado;
    }

    public async Task<MovimentacoesArquivos[]> BaixarMovimentacoes(int codigoSolicitacao, int[] arquivos)
    {
        var lst = new List<MovimentacoesArquivos>();
        foreach (var idArquivo in arquivos)
        {
            var retorno = await DownloadArquivoMovimentacao(codigoSolicitacao, idArquivo);

            var bytesZip = Convert.FromBase64String(retorno.arquivo);
            SalvarCopiaMovimentacao(bytesZip, retorno.nomeArquivo);

            var registros = Helpers.ProcessarArquivoMovimentacao(bytesZip);
            lst.Add(new MovimentacoesArquivos
            {
                codigoSolicitacao = codigoSolicitacao,
                idArquivo = idArquivo,
                nomeArquivo = retorno.nomeArquivo,
                Movimentacoes = registros.ToArray(),
            });
        }
        return lst.ToArray();
    }

    /* Webhook */

    /// <summary>
    /// Consultar os webhooks cadastrados.
    /// Serviço para consultar os detalhes dos webhooks cadastrados.
    /// </summary>
    /// <param name="idWebhook">Identificador único do webhook.</param>
    /// <param name="codigoTipoMovimento">Código do tipo de movimento do webhook. 7 - Pagamento (Baixa operacional)</param>
    /// <returns>Webhooks cadastradros.</returns>
    public async Task<ConsultaWebhookResponse> ConsultarWebHooksAsync(long? idWebhook = null, int? codigoTipoMovimento = null)
    {
        var consulta = new ConsultaWebhookRequest()
        {
            idWebhook = idWebhook,
            codigoTipoMovimento = codigoTipoMovimento
        };
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaWebhookResponse>(ConfigApi.UrlApi + "cobranca-bancaria/v3/webhooks", consulta));
    }

    /// <summary>
    /// Cadastrar um webhook para receber notificações de acordo com o tipo de movimento.
    /// Este serviço permite cadastrar uma URL que será notificada sempre que ocorrer um evento associado a um tipo de movimento. O webhook pode ser configurado para o período de movimentação atual (D0).
    /// </summary>
    /// <param name="url">Url a ser chamada com POST. A URL deve ser https.</param>
    /// <param name="email">E-mail associado ao webhook.</param>
    /// <param name="codigoTipoMovimento">Código do tipo de movimento do webhook. 7 - Pagamento (Baixa operacional)</param>
    /// <param name="codigoPeriodoMovimento">Código do período de movimento. 1 - Movimento atual (D0) </param>
    /// <returns></returns>
    public async Task<IncluirWebhooksResponse?> CriarWebHookAsync(string url, string email, int codigoTipoMovimento = 7, int codigoPeriodoMovimento = 1) {
        IncluirWebhookRequest webhook = new IncluirWebhookRequest()
        {
            url = url,
            email = email,
            codigoTipoMovimento = codigoTipoMovimento,
             codigoPeriodoMovimento = codigoPeriodoMovimento
        };
        return await ExecutaChamadaAsync(() => clientApi.PostAsync<IncluirWebhooksResponse?>(ConfigApi.UrlApi + "cobranca-bancaria/v3/webhooks", webhook));
    }

    /// <summary>
    /// Atualizar um webhook cadastrado.
    /// Serviço de atualização de webhook. Ao modificar a URL, a situação do webhook será automaticamente alterada para '1 - Aguardando validação' e permanecerá assim até que a nova URL seja validada com sucesso.
    /// </summary>
    /// <param name="idWebhook">Identificador único do webhook.</param> 
    /// <param name="webhook">Dados para alteração do webhook.</param>
    /// <returns></returns>     
    public async Task AlterarWebhookAsync(long idWebhook, AlterarWebhookRequest webhook)
        => await ExecutaChamadaAsync(() => clientApi.PatchAsync(ConfigApi.UrlApi + "cobranca-bancaria/v3/webhooks/" + idWebhook, webhook));
    

    /// <summary>
    /// Excluir um webhook.
    /// Serviço responsável por remover permanentemente um webhook registrado, encerrando o envio de notificações para a URL vinculada."
    /// </summary>
    /// <param name="idWebhook">Identificador único do webhook.</param>
    /// <returns></returns>
    public async Task ExcluirWebhookAsync(long idWebhook)
        => await ExecutaChamadaAsync(() => clientApi.DeleteAsync(ConfigApi.UrlApi + "cobranca-bancaria/v3/webhooks/" + idWebhook));

    /// <summary>
    /// Reativar um webhook inativo.
    /// Serviço de reativação de webhook desativado, restabelecendo o recebimento de notificações. A situação do webhook será atualizada para '1 - Aguardando validação' e permanecerá assim até que a URL seja validada com sucesso.
    /// </summary>
    /// <param name="idWebhook">Identificador único do webhook.</param>     
    /// <returns></returns>
    public async Task ReativarWebhookAsync(long idWebhook)
        => await ExecutaChamadaAsync(() => clientApi.PatchAsync(ConfigApi.UrlApi + "cobranca-bancaria/v3/webhooks/" + idWebhook + "/reativar", null));
    
    /// <summary>
    /// Consultar solicitações de um webhook.
    /// Consulta as solicitações de notificação para um webhook com base na data de solicitação informada.
    /// </summary>
    /// <param name="idWebhook">Identificador único do webhook.</param>
    /// <param name="dataSolicitacao">Data da solicitação. Formato: yyyy-MM-dd</param>
    /// <param name="pagina">Número da página a ser consultada.</param>
    /// <param name="codigoSolicitacaoSituacao">Código da situação da solicitação do webhook. 2 - Aguardando envio, 3 - Enviado com sucesso e 6 - Erro no envio</param>
    /// <param name="codigoBarras">Código de barras do boleto presente na notificação webhook</param>
    /// <param name="nossoNumero">Nosso número do boleto presente na notificação webhook</param>
    /// <returns>Retorna o histórico das tentativas de notificação, incluindo o status e a resposta da requisição.</returns>
    public async Task<ConsultaSolicitacoesWebhookResponse> SolicitacoesWebhooksAsync(long idWebhook, DateTime dataSolicitacao, int? pagina = null, int? codigoSolicitacaoSituacao = null, string? codigoBarras = null, int? nossoNumero = null)
    {
        var consulta = new ConsultaSolicitacoesWebhookRequest()
        {
            dataSolicitacao = dataSolicitacao,
            codigoBarras = codigoBarras,
            nossoNumero = nossoNumero,
            codigoSolicitacaoSituacao = codigoSolicitacaoSituacao,
            pagina = pagina
        };
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaSolicitacoesWebhookResponse>(ConfigApi.UrlApi + "cobranca-bancaria/v3/webhooks/" + idWebhook + "/solicitacoes", consulta));
    }

    private void SalvarCopiaMovimentacao(byte[] bytesZip, string nomeArquivo)
    {
        if (PastaCopiaMovimentacoes == null) return;
        if (!Directory.Exists(PastaCopiaMovimentacoes)) Directory.CreateDirectory(PastaCopiaMovimentacoes);

        var path = Path.Combine(PastaCopiaMovimentacoes, nomeArquivo);
        if (File.Exists(path)) return;

        File.WriteAllBytes(path, bytesZip);
    }
}
