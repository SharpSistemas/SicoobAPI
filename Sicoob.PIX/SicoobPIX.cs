using Sicoob.Shared;
using Sicoob.Shared.Models.Acesso;
using Simple.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sicoob.PIX
{
    public sealed class SicoobPIX : Shared.Sicoob
    {
        private ClientInfo clientApi;
        public Shared.Models.ConfiguracaoAPI ConfigApi { get; }

        public SicoobPIX(Shared.Models.ConfiguracaoAPI configApi)
            : base(configApi)
        {
            ConfigApi = configApi;
        }

        protected override void setupClients(HttpClientHandler handler)
        {
            clientApi = new ClientInfo(ConfigApi.UrlApi, handler);
        }
        protected override void atualizaClients(TokenResponse token)
        {
            clientApi.SetAuthorizationBearer(token.access_token);
        }

        /* COB Payload */

        /* COB */
        public async Task<Models.Cobranca.CriarCobrancaResponse> CriarCobranca(string transactionId, Models.Cobranca.CriarCobrancaRequest cobranca) => await ExecutaChamadaAsync(()
            => clientApi.PutAsync<Models.Cobranca.CriarCobrancaResponse>($"/pix/api/v2/cob/{transactionId}", cobranca));

        /* PIX */
        public async Task<Models.Pix.ConsultaResponse> ConsultarPIXAsync(Models.Pix.ConsultaRequest consulta)
            => await ExecutaChamadaAsync(() => clientApi.GetAsync<Models.Pix.ConsultaResponse>("/pix/api/v2/pix", consulta.ToKVP()));
        public async Task<Models.Pix.PixResponse> ConsultarPIXAsync(string endToEndId)
             => await ExecutaChamadaAsync(() => clientApi.GetAsync<Models.Pix.PixResponse>($"/pix/api/v2/pix/{endToEndId}"));


    }
}
