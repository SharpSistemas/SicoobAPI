using Simple.API;
using System.Threading.Tasks;

namespace Sicoob.PIX
{
    /// <summary>
    /// Consulta os endpoints abertos (Sem autenticação)
    /// </summary>
    public class SicoobConsultaPayload
    {
        private readonly ClientInfo clientApi;
        private readonly ClientInfo clientPix;
        public SicoobConsultaPayload()
        {
            clientApi = new ClientInfo("https://api.sicoob.com.br/pix/api/v2/");
            clientPix = new ClientInfo("https://pix.sicoob.com.br");
        }
        /// <summary>
        /// Consulta um Location
        /// </summary>
        /// <returns>JWT contendo os dados do PIX</returns>
        public async Task<string> ConsultaCobPayloadAsync(string urlAcessToken)
        {
            var response = await clientApi.GetAsync<string>(urlAcessToken);
            response.EnsureSuccessStatusCode();
            return response.Data;
        }
        /// <summary>
        /// Consulta campo payloadURL do PIX
        /// </summary>
        /// <returns>JWT contendo os dados do PIX</returns>
        public async Task<string> ConsultaPixPayloadUrlAsync(string url)
        {
            url = url.Substring(url.IndexOf("/") + 1);
            var response = await clientPix.GetAsync<string>(url);
            response.EnsureSuccessStatusCode();
            return response.Data;
        }

    }
}
