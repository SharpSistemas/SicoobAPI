using Simple.API;
using System.Net.Http;
using System.Threading.Tasks;
using Sicoob.Shared;
using Sicoob.Shared.Models.Acesso;

namespace Sicoob.PIX
{
    public sealed class SicoobPIX : Shared.Sicoob
    {
        private ClientInfo clientApi;
        public Models.ConfiguracaoPIX ConfigApi { get; }

        public SicoobPIX(Models.ConfiguracaoPIX configApi)
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

        public async Task<Models.Pix.ConsultaResponse> ConsultarPIXAsync(Models.Pix.ConsultaRequest consulta)
        {
            await this.VeiricaAtualizaCredenciaisAsync();
            var response = await clientApi.GetAsync<Models.Pix.ConsultaResponse>("/pix/api/v2/pix", consulta.ToKVP());
            response.EnsureSuccessStatusCode();
            return response.Data;
        }
        public async Task<Models.Pix.PixResponse> ConsultarPIXAsync(string endToEndId)
        {
            await this.VeiricaAtualizaCredenciaisAsync();
            var response = await clientApi.GetAsync<Models.Pix.PixResponse>($"/pix/api/v2/pix/{endToEndId}");
            response.EnsureSuccessStatusCode();
            return response.Data;
        }

    }
}
