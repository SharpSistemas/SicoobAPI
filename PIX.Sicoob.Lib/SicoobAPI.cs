
using Simple.API;
using System.Net.Http;
using System.Threading.Tasks;

namespace PIX.Sicoob.Lib
{
    public class SicoobAPI
    {
        private readonly Models.ConfiguracaoAPI config;
        private ClientInfo clientAuth;
        private ClientInfo clientApi;

        public SicoobAPI(Models.ConfiguracaoAPI config)
        {
            this.config = config ?? throw new System.ArgumentNullException(nameof(config));
        }

        public async Task SetupAsync()
        {
        }

    }
}
