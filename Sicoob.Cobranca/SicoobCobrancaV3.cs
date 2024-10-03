/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/

using Sicoob.Cobranca.Models.Shared;
using Sicoob.Cobranca.Models.v2;
using Sicoob.Cobranca.Models.v3;
using Sicoob.Shared.Models;
using BaixarBoletoRequest = Sicoob.Cobranca.Models.v3.BaixarBoletoRequest;
using ConsultaBoletoRequest = Sicoob.Cobranca.Models.v3.ConsultaBoletoRequest;
using ConsultaBoletoResponse = Sicoob.Cobranca.Models.v3.ConsultaBoletoResponse;
using ConsultaBoletosPagadorResponse = Sicoob.Cobranca.Models.v3.ConsultaBoletosPagadorResponse;
using IncluirBoletosResponse = Sicoob.Cobranca.Models.v3.IncluirBoletosResponse;

namespace Sicoob.Cobranca;

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
    private Shared.Models.ConfiguracaoAPI ConfigApi { get; }
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
    
    public async Task BaixarBoletos(int nossoNumero, int codigoModalidade)
    {
        var baixa = new BaixarBoletoRequest
        {
            numeroCliente = numeroContrato,
            codigoModalidade = codigoModalidade
        };
        await ExecutaChamadaAsync(() => clientApi.PatchAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/boletos/{nossoNumero}/baixar", baixa));
    }
    
    public async Task ProtestarBoletos(int nossoNumero, int codigoModalidade)
    {
        var protesto = new ProtestoRequest
        {
            numeroCliente = numeroContrato,
            codigoModalidade = codigoModalidade,
        };        
        await ExecutaChamadaAsync(() => clientApi.PostAsync(ConfigApi.UrlApi + $"cobranca-bancaria/v3/boletos/{nossoNumero}/protestos", protesto));
    }
    
    public async Task AlterarBoleto(int nossoNumero, AlterarBoletoRequest boletos)
    {
        await ExecutaChamadaAsync(() => clientApi.PatchAsync(ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos/" + nossoNumero, boletos));
    }

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
        var result = await clientApi.GetAsync<ResponseMovimentacao<RetornoConsultaMovimentacoes>>( ConfigApi.UrlApi + "cobranca-bancaria/v3/boletos/solicitacoes/movimentacao", new { numeroCliente = numeroContrato, codigoSolicitacao });

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
        var retorno = await ExecutaChamadaAsync(() => clientApi.GetAsync<ResponseMovimentacao<RetornoArquivoMovimentacao>>(ConfigApi.UrlApi + "/cobranca-bancaria/v3/boletos/movimentacoes/download", new { numeroCliente = numeroContrato, codigoSolicitacao, idArquivo }));
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

    private void SalvarCopiaMovimentacao(byte[] bytesZip, string nomeArquivo)
    {
        if (PastaCopiaMovimentacoes == null) return;
        if (!Directory.Exists(PastaCopiaMovimentacoes)) Directory.CreateDirectory(PastaCopiaMovimentacoes);

        var path = Path.Combine(PastaCopiaMovimentacoes, nomeArquivo);
        if (File.Exists(path)) return;

        File.WriteAllBytes(path, bytesZip);
    }
}
