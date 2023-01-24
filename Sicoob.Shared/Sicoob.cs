using Simple.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sicoob.Shared
{
    public abstract class Sicoob
    {
        private readonly Models.Configuracao config;
        private readonly HttpClientHandler httpHandler;
        private ClientInfo clientAuth;

        public DateTime ExpiresAtUTC { get; private set; }
        public TimeSpan ExpiresIn => ExpiresAtUTC - DateTime.UtcNow;
        public bool Expired => ExpiresIn.TotalSeconds < 0;

        public Sicoob(Models.Configuracao config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            var x509 = new System.Security.Cryptography.X509Certificates.X509Certificate2(config.UrlCertificadoPFX, config.CertificadoSenha);
            httpHandler = new HttpClientHandler();
            httpHandler.ClientCertificates.Add(x509);

            clientAuth = new ClientInfo(config.UrlAutenticacao, httpHandler);
        }

        protected void enableDebug(ClientInfo clientApi)
        {
            clientApi.BeforeSend += ClientApi_BeforeSend;
            clientApi.ResponseDataReceived += ClientApi_ResponseDataReceived;
            debugLog("[SETUP]", clientApi.BaseUri.ToString());
        }
        private void ClientApi_ResponseDataReceived(object sender, ClientInfo.ResponseReceived e)
        {
            debugLog("<<", $"[{e.StatusCode}] RECV: {e.Content}");
        }
        private void ClientApi_BeforeSend(object sender, HttpRequestMessage e)
        {
            string content = "";
            if (e.Content != null)
            {
                if (e.Content is StringContent strCnt)
                {
                    content = strCnt.ReadAsStringAsync().Result;
                }
            }

            debugLog(">>", $"[{e.Method}] {e.RequestUri} {content}");
        }
        private void debugLog(string direciton, string content)
        {
            System.IO.File.AppendAllText("debug.log", $"{DateTime.Now:G} {direciton} {content}\r\n");
        }


        public async Task SetupAsync()
        {
            setupClients(httpHandler);
            await atualizaCredenciaisAsync();
        }
        protected abstract void setupClients(HttpClientHandler handler);
        protected abstract void atualizaClients(Models.Acesso.TokenResponse token);

        public async Task AtualizarCredenciaisAsync()
            => await atualizaCredenciaisAsync();
        private async Task atualizaCredenciaisAsync()
        {
            var response = await clientAuth.FormUrlEncodedPostAsync<Models.Acesso.TokenResponse>("token", new
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
        protected async Task<T> ExecutaChamadaAsync<T>(Func<Task<Response<T>>> func)
        {
            await VeiricaAtualizaCredenciaisAsync();
            Response<T> response = await func();
            response.EnsureSuccessStatusCode();
            return response.Data;
        }
        protected async Task ExecutaChamadaAsync(Func<Task<Response>> func)
        {
            await VeiricaAtualizaCredenciaisAsync();
            Response response = await func();
            response.EnsureSuccessStatusCode();
        }
    }
}
