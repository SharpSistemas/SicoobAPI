using Simple.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sicoob.Shared
{
    public abstract class Sicoob
    {
        private readonly Models.Configuracao config;
        private ClientInfo clientAuth;

        public DateTime ExpiresAtUTC { get; private set; }
        public TimeSpan ExpiresIn => ExpiresAtUTC - DateTime.UtcNow;
        public bool Expired => ExpiresIn.TotalSeconds < 0;

        public Sicoob(Models.Configuracao config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task SetupAsync()
        {
            var handler = new HttpClientHandler();

            var x509 = new System.Security.Cryptography.X509Certificates.X509Certificate2(config.UrlCertificadoPFX, config.CertificadoSenha);
            handler.ClientCertificates.Add(x509);

            clientAuth = new ClientInfo(config.UrlAutenticacao, handler);
            setupClients(handler);

            await atualizaCredenciaisAsync();
        }
        protected abstract void setupClients(HttpClientHandler handler);
        protected abstract void atualizaClients(Models.Acesso.TokenResponse token);

        public async Task AtualizarCredenciaisAsync()
            => await atualizaCredenciaisAsync();
        private async Task atualizaCredenciaisAsync()
        {
            var response = await clientAuth.FormUrlEncodedPostAsync<Shared.Models.Acesso.TokenResponse>("token", new
            {
                client_id = config.ClientId,
                grant_type = "client_credentials",
                scope = config.Scope.ToScopeString(),
            });
            response.EnsureSuccessStatusCode();
            atualizaClients(response.Data);
            ExpiresAtUTC = DateTime.UtcNow.AddSeconds(response.Data.expires_in);
        }

        protected async Task<bool> VeiricaAtualizaCredenciaisAsync()
        {
            if (ExpiresIn.TotalSeconds >= 5)
            {
                return false;
            }

            await atualizaCredenciaisAsync();
            return true;
        }
    }
}
