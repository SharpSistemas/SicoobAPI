using Sicoob.Shared.Models.Acesso;
using Simple.API;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sicoob.Conta
{
    public sealed class SicoobContaPoupanca : Shared.Sicoob
    {
        // Documentações
        // > APIs tipo "Swagger":
        //   https://developers.sicoob.com.br/#!/apis
        // > APIs no POSTMAN:
        //   https://documenter.getpostman.com/view/20565799/UzBnrmod#239d9f68-d646-4209-994a-cd564b6d6d1a

        private ClientInfo clientApi;
        public Shared.Models.ConfiguracaoAPI ConfigApi { get; }

        public SicoobContaPoupanca(Shared.Models.ConfiguracaoAPI configApi)
            : base(configApi)
        {
            ConfigApi = configApi;
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

        public async Task<string> ObterSaldoAsync(string contaPoupanca)
            => await ExecutaChamadaAsync(() => clientApi.GetAsync<string>($"/poupanca/v1/contas/{contaPoupanca}/saldo"));
        public async Task<string> ObterExtratoAsync(int mes, int ano, string contaPoupanca)
            => await ExecutaChamadaAsync(() => clientApi.GetAsync<string>($"/poupanca/v1/contas/{contaPoupanca}/extrato/{mes}/{ano}"));

    }
}
