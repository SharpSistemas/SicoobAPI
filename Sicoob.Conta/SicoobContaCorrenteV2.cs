/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/

using Sicoob.Conta.Models.Shared;
using Sicoob.Conta.Models.v2;

namespace Sicoob.Conta;

using Sicoob.Conta.Models;
using Sicoob.Shared.Models.Acesso;
using Simple.API;
using System.Net.Http;
using System.Threading.Tasks;

public sealed class SicoobContaCorrenteV2 : Shared.Sicoob
{
    // Documentações
    // > APIs tipo "Swagger":
    //   https://developers.sicoob.com.br/#!/apis

    private readonly int numeroContaCorrente;
    private ClientInfo clientApi;

    public Shared.Models.ConfiguracaoAPI ConfigApi { get; }

    public SicoobContaCorrenteV2(Shared.Models.ConfiguracaoAPI configApi, int NumeroContaCorrente, System.Security.Cryptography.X509Certificates.X509Certificate2? certificado = null)
        : base(configApi, certificado)
    {
        ConfigApi = configApi;
        numeroContaCorrente = NumeroContaCorrente;
    }

    protected override void setupClients(HttpClientHandler handler)
    {
        clientApi = new ClientInfo(ConfigApi.UrlApi, handler);
        clientApi.SetHeader("x-sicoob-clientid", ConfigApi.ClientId);

#if DEBUG
        enableDebug(clientApi);
#endif
    }
    protected override void atualizaClients(TokenResponse token)
    {
        clientApi.SetAuthorizationBearer(token.access_token); // Deveria ser o id_token, mas dá erro de falta de scope
    }

    /// <summary>
    /// O recurso de Saldo retorna o valor disponível atual e o limite de crédito (cheque especial) de uma conta corrente.
    /// </summary>
    public async Task<ResultadoResponse<SaldoResponseBase>> ObterSaldoAsync()
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<ResultadoResponse<SaldoResponseBase>>("/conta-corrente/v2/saldo", new { numeroContaCorrente }));
    

    /// <summary>
    /// O recurso de Extrato retorna todas as transações ocorridas em uma conta corrente no devido mês e ano.
    /// Há um limite de 3 meses
    /// </summary>
    public async Task<ResultadoResponse<ExtratoResponse>> ObterExtratoAsync(int mes, int ano)
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<ResultadoResponse<ExtratoResponse>>($"/conta-corrente/v2/extrato/{mes}/{ano}", new { numeroContaCorrente }));

}
