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
        // > APIs no POSTMAN:
        //   https://documenter.getpostman.com/view/20565799/UzBnrmod#239d9f68-d646-4209-994a-cd564b6d6d1a

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
            clientApi.SetAuthorizationBearer(token.access_token);
        }

        public async Task<string> ObterSaldoAsync() 
            => await ExecutaChamadaAsync(() => clientApi.GetAsync<string>("/conta-corrente/v2/saldo"));

    }
}
