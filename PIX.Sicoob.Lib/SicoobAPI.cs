using Simple.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sicoob.PIX.Lib
{
    public class SicoobAPI
    {
        private readonly Models.ConfiguracaoAPI config;
        private ClientInfo clientAuth;
        private ClientInfo clientApi;

        public DateTime ExpiresAtUTC { get; private set; }

        public SicoobAPI(Models.ConfiguracaoAPI config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task SetupAsync()
        {
            var handler = new HttpClientHandler();

            var x509 = new System.Security.Cryptography.X509Certificates.X509Certificate2(config.UrlCertificadoPFX, config.CertificadoSenha);
            handler.ClientCertificates.Add(x509);

            clientAuth = new ClientInfo(config.UrlAutenticacao, handler);
            clientApi = new ClientInfo(config.UrlApi, handler);

            await atualizaCredenciaisAsync();

            clientApi.SetHeader("x-sicoob-clientid", config.ClientId);
        }
        private async Task atualizaCredenciaisAsync()
        {
            var response = await clientAuth.FormUrlEncodedPostAsync<Models.Acesso.TokenResponse>("token", new
            {
                client_id = config.ClientId,
                grant_type = "client_credentials",
                scope = config.Scope.ToScopeString(),
            });
            response.EnsureSuccessStatusCode();

            clientApi.SetAuthorizationBearer(response.Data.access_token);
            ExpiresAtUTC = DateTime.UtcNow.AddSeconds(response.Data.expires_in);
        }
        public async Task AtualizarCredenciaisAsync()
            => await atualizaCredenciaisAsync();

        public async Task<Models.Pix.ConsultaResponse> ConsultarPIX(Models.Pix.ConsultaRequest consulta)
        {
            var response = await clientApi.GetAsync<Models.Pix.ConsultaResponse>("pix", consulta.ToKVP());
            response.EnsureSuccessStatusCode();
            return response.Data;
        }

        public async Task<Models.Pix.PixResponse> ConsultarPIX(string endToEndId)
        {
            var response = await clientApi.GetAsync<Models.Pix.PixResponse>($"pix/{endToEndId}");
            response.EnsureSuccessStatusCode();
            return response.Data;
        }
    }
}
