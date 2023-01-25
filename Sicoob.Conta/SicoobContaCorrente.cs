/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using Sicoob.Conta.Models;
using Sicoob.Shared.Models.Acesso;
using Simple.API;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sicoob.Conta
{
    public sealed class SicoobContaCorrente : Shared.Sicoob
    {
        // Documentações
        // > APIs tipo "Swagger":
        //   https://developers.sicoob.com.br/#!/apis

        private ClientInfo clientApi;
        public Shared.Models.ConfiguracaoAPI ConfigApi { get; }

        public SicoobContaCorrente(Shared.Models.ConfiguracaoAPI configApi)
            : base(configApi)
        {
            ConfigApi = configApi;
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
            clientApi.SetAuthorizationBearer(token.id_token);
        }

        /// <summary>
        /// O recurso de Saldo retorna o valor disponível atual e o limite de crédito (cheque especial) de uma conta corrente.
        /// </summary>
        public async Task<ResultadoResponse<SaldoResponse>> ObterSaldoAsync()
            => await ExecutaChamadaAsync(() => clientApi.GetAsync<ResultadoResponse<SaldoResponse>>("/conta-corrente/v2/saldo"));
        /// <summary>
        /// O recurso de Saldo retorna o valor disponível atual e o limite de crédito (cheque especial) de uma conta corrente.
        /// </summary>
        public async Task<ResultadoResponse<SaldoResponse>> ObterSaldoAsync(string numeroContaCorrente)
            => await ExecutaChamadaAsync(() => clientApi.GetAsync<ResultadoResponse<SaldoResponse>>("/conta-corrente/v2/saldo", new { numeroContaCorrente }));
        
        /// <summary>
        /// O recurso de Extrato retorna todas as transações ocorridas em uma conta corrente no devido mês e ano.
        /// Há um limite de 3 meses
        /// </summary>
        public async Task<ResultadoResponse<ExtratoResponse>> ObterExtratoAsync(int mes, int ano)
            => await ExecutaChamadaAsync(() => clientApi.GetAsync<ResultadoResponse<ExtratoResponse>>($"/conta-corrente/v2/extrato/{mes}/{ano}"));
        /// <summary>
        /// O recurso de Extrato retorna todas as transações ocorridas em uma conta corrente no devido mês e ano.
        /// Há um limite de 3 meses
        /// </summary>
        public async Task<ResultadoResponse<ExtratoResponse>> ObterExtratoAsync(int mes, int ano, string numeroContaCorrente)
            => await ExecutaChamadaAsync(() => clientApi.GetAsync<ResultadoResponse<ExtratoResponse>>($"/conta-corrente/v2/extrato/{mes}/{ano}", new { numeroContaCorrente }));

    }
}
