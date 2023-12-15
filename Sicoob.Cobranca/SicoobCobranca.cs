/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Cobranca;

using Sicoob.Cobranca.Models;
using Sicoob.Shared.Models.Acesso;
using Simple.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Classe para comunicação com as APIs de Cobrança do Sicoob
/// </summary>
public sealed class SicoobCobranca : Shared.Sicoob
{
    // Documentações
    // > APIs tipo "Swagger":
    //   https://developers.sicoob.com.br/#!/apis
    // > Link que o Suporte do Sicoob enviou
    // https://documenter.getpostman.com/view/20565799/Uzs6yNhe#6447c293-f67b-44ba-b7be-41f5c3de978d

    private ClientInfo clientApi;
    public Shared.Models.ConfiguracaoAPI ConfigApi { get; }

    public SicoobCobranca(Shared.Models.ConfiguracaoAPI configApi, System.Security.Cryptography.X509Certificates.X509Certificate2? certificado = null)
       : base(configApi, certificado)
    {
        ConfigApi = configApi;
        clientApi = new ClientInfo(ConfigApi.UrlApi);
    }

    protected override void setupClients(HttpClientHandler handler)
    {
        clientApi = new ClientInfo(ConfigApi.UrlApi, handler);
        clientApi.SetHeader("x-sicoob-clientid", ConfigApi.ClientId);
    }
    protected override void atualizaClients(TokenResponse token)
    {
        clientApi.SetAuthorizationBearer(token.access_token);
    }

    /* Boletos */

    /// <summary>
    /// Consulta boleto utilizando um dos três métodos de busca
    /// </summary>
    /// <param name="numeroContrato">Número que identifica o contrato do beneficiário no Sisbr</param>
    /// <param name="nossoNumero">Número identificador do boleto no Sisbr. Caso seja infomado, não é necessário infomar a linha digitável ou código de barras</param>
    /// <param name="linhaDigitavel">Número da linha digitável do boleto com 47 posições. Caso seja informado, não é necessário informar o nosso número ou código de barras</param>
    /// <param name="codigoBarras">Número de código de barras do boleto com 44 posições.Caso seja informado, não é necessário informar o nosso número ou linha digitável</param>
    /// <returns>Boleto buscado</returns>
    public async Task<ConsultaBoletoResponse?> ConsultarBoleto(int numeroContrato, int? nossoNumero = null, string? linhaDigitavel = null, string? codigoBarras = null)
    {
        var consulta = new ConsultaBoletoRequest()
        {
            modalidade = 1,
            numeroContrato = numeroContrato,
            nossoNumero = nossoNumero,
            linhaDigitavel = linhaDigitavel,
            codigoBarras = codigoBarras
        };
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaBoletoResponse?>("/cobranca-bancaria/v2/cobranca-bancaria/v2/boletos", consulta));
    }

    /// <summary>
    /// Consulta boletos de um Pagador
    /// </summary>
    /// <param name="numeroCpfCnpj">CPF/CNPJ do Pagador</param>
    /// <param name="numeroContrato">Número que identifica o contrato do beneficiário no Sisbr</param>
    /// <param name="codigoSituacao">Código da Situação do Boleto. 1: Em aberto, 2: Baixado, 3: Liquidado</param>
    /// <param name="dataVencimentoInicio">Data de Vencimento Inicial</param>
    /// <param name="dataVencimentoFim">Data de Vencimento Final</param>
    /// <returns>Boletos do Pagador</returns>
    public async Task<ConsultaBoletosPagadorResponse> ConsultarBoletosPagador(int numeroContrato, string numeroCpfCnpj, int? codigoSituacao = null, DateTime? dataVencimentoInicio = null, DateTime? dataVencimentoFim = null)
    {
        var consulta = new ConsultaBoletosPagadorRequest()
        {
            numeroContrato = numeroContrato,
            codigoSituacao = codigoSituacao,
            dataInicio = dataVencimentoInicio?.ToString("yyyy-MM-dd"),
            dataFim = dataVencimentoFim?.ToString("yyyy-MM-dd")
        };
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaBoletosPagadorResponse>("/cobranca-bancaria/v2/cobranca-bancaria/v2/boletos/pagadores/" + numeroCpfCnpj, consulta));
    }

    public async Task<ConsultaBoletoResponse?> ConsultarSegundaViaBoleto(int numeroContrato, int modalidade, int? nossoNumero = null, string? linhaDigitavel = null, string? codigoBarras = null, bool gerarPdf = false)
    {
        var consulta = new ConsultaBoletoRequest()
        {
            modalidade = modalidade,
            numeroContrato = numeroContrato,
            nossoNumero = nossoNumero,
            linhaDigitavel = linhaDigitavel,
            codigoBarras = codigoBarras,
            gerarPdf = gerarPdf
        };
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaBoletoResponse?>("/cobranca-bancaria/v2/cobranca-bancaria/v2/boletos/segunda-via", consulta));
    }

    public async Task<IncluirBoletosResponse?> IncluirBoletos(IncluirBoletosRequest[] boletos)
    {
        return await ExecutaChamadaAsync(() => clientApi.PostAsync<IncluirBoletosResponse?>("/cobranca-bancaria/v2/cobranca-bancaria/v2/boletos", boletos));
    }

    /* Movimentação */
    public async Task<RetornoSolicitacaoMovimentacoesCarteira> SolicitarMovimentacao(int numeroContrato, int tipoMovimento, DateTime data)
    {
        var di = data.Date;
        var df = data.Date.AddDays(1).AddSeconds(-1);
        return await SolicitarMovimentacao(numeroContrato, tipoMovimento, di, df);
    }
    public async Task<RetornoSolicitacaoMovimentacoesCarteira> SolicitarMovimentacao(int numeroContrato, int tipoMovimento, DateTime dataInicial, DateTime dataFinal)
        => await SolicitarMovimentacao(new SolicitacaoMovimentacoesCarteira() { numeroContrato = numeroContrato, tipoMovimento = tipoMovimento, dataInicial = dataInicial, dataFinal = dataFinal });
    private async Task<RetornoSolicitacaoMovimentacoesCarteira> SolicitarMovimentacao(SolicitacaoMovimentacoesCarteira solicitacao)
    {
        var retorno = await ExecutaChamadaAsync(() => clientApi.PostAsync<ResponseMovimentacao<RetornoSolicitacaoMovimentacoesCarteira>>("/cobranca-bancaria/v2/boletos/solicitacoes/movimentacao", solicitacao));
        return retorno.resultado;
    }
    public async Task<RetornoConsultaMovimentacoes> ConsultarSituacaoSolicitacao(int numeroContrato, int codigoSolicitacao)
    {
        var retorno = await ExecutaChamadaAsync(() => clientApi.GetAsync<ResponseMovimentacao<RetornoConsultaMovimentacoes>>("/cobranca-bancaria/v2/boletos/solicitacoes/movimentacao", new { numeroContrato, codigoSolicitacao }));
        return retorno.resultado;
    }
    internal async Task<RetornoArquivoMovimentacao> DownloadArquivoMovimentacao(int numeroContrato, int codigoSolicitacao, int idArquivo)
    {
        var retorno = await ExecutaChamadaAsync(() => clientApi.GetAsync<ResponseMovimentacao<RetornoArquivoMovimentacao>>("/cobranca-bancaria/v2/boletos/movimentacao-download", new { numeroContrato, codigoSolicitacao, idArquivo }));
        return retorno.resultado;
    }
    public async Task<MovimentacoesArquivo[]> BaixarMovimentacoes(int numeroContrato, int codigoSolicitacao, int[] arquivos)
    {
        var lst = new List<MovimentacoesArquivo>();
        foreach (var idArquivo in arquivos)
        {
            var retorno = await DownloadArquivoMovimentacao(numeroContrato, codigoSolicitacao, idArquivo);
            var registros = Helpers.ProcessarArquivoMovimentacao(retorno.arquivo);
            lst.AddRange(registros);
        }
        return lst.ToArray();
    }
}
