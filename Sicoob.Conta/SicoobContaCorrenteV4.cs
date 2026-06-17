using System;
using System.Net.Http;
using System.Threading.Tasks;
using Sicoob.Conta.Models;
using Sicoob.Conta.Models.Shared;
using Sicoob.Conta.Models.v4;
using Sicoob.Shared.Models;
using Sicoob.Shared.Models.Acesso;
using Simple.API;

namespace Sicoob.Conta;

public class SicoobContaCorrenteV4 : Shared.Sicoob
{
    // Documentações
    // > APIs tipo "Swagger":
    //   https://developers.sicoob.com.br/#!/apis

    private readonly int numeroContaCorrente;
    private ClientInfo clientApi;
    public delegate void UpdateToken(ConfiguracaoToken token);
    public event UpdateToken? UpdateTokenEvent;

    public Shared.Models.ConfiguracaoAPI ConfigApi { get; }

    public SicoobContaCorrenteV4(Shared.Models.ConfiguracaoAPI configApi, int NumeroContaCorrente, System.Security.Cryptography.X509Certificates.X509Certificate2? certificado = null)
        : base(configApi, certificado)
    {
        ConfigApi = configApi;
        numeroContaCorrente = NumeroContaCorrente;
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

    /// <summary>
    /// O recurso de Saldo retorna o valor disponível atual e o limite de crédito (cheque especial) de uma conta corrente.
    /// </summary>
    public async Task<ResultadoResponse<SaldoResponse>> ObterSaldoAsync()
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<ResultadoResponse<SaldoResponse>>("/conta-corrente/v4/saldo", new { numeroContaCorrente }));
    

    /// <summary>
    /// O recurso de Extrato retorna todas as transações ocorridas em uma conta corrente no devido mês e ano.
    /// Há um limite de 3 meses
    /// </summary>
    public async Task<ResultadoResponse<ExtratoResponse>> ObterExtratoAsync(int mes, int ano, int? diaInicial = null, int? diaFinal = null)
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<ResultadoResponse<ExtratoResponse>>($"/conta-corrente/v4/extrato/{mes}/{ano}", new { numeroContaCorrente, diaInicial, diaFinal }));

}