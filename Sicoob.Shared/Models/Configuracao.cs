using System.Collections.Generic;

namespace Sicoob.Shared.Models
{
    public class Configuracao
    {
        public string ClientId { get; set; }

        public string UrlAutenticacao { get; set; } = "https://auth.sicoob.com.br/auth/realms/cooperado/protocol/openid-connect/";
        public string UrlCertificadoPFX { get; set; }
        public string CertificadoSenha { get; set; }
        public AuthorizationScope Scope { get; set; } = new AuthorizationScope();

    }
    public class AuthorizationScope
    {
        /*
        cob.write: Permissão para alteração de cobranças imediatas

        cob.read: Permissão para consulta de cobranças imediatas

        cobv.write: Permissão para alteração de cobranças com vencimento

        cobv.read: Permissão para consulta de cobranças com vencimento

        lotecobv.write: Permissão para alteração de lotes de cobranças com vencimento

        lotecobv.read: Permissão para consulta de lotes de cobranças com vencimento
        */

        /// <summary>
        /// Permissão para alteração de cobranças imediatas
        /// </summary>
        public bool COB_WRITE { get; set; }
        /// <summary>
        /// Permissão para consulta de cobranças imediatas
        /// </summary>
        public bool COB_READ { get; set; }
        /// <summary>
        /// Permissão para alteração de cobranças com vencimento
        /// </summary>
        public bool COBV_WRITE { get; set; }
        /// <summary>
        /// Permissão para consulta de cobranças com vencimento
        /// </summary>
        public bool COBV_READ { get; set; }
        /// <summary>
        /// Permissão para alteração de lotes de cobranças com vencimento
        /// </summary>
        public bool LOTE_COBV_WRITE { get; set; }
        /// <summary>
        /// Permissão para consulta de lotes de cobranças com vencimento
        /// </summary>
        public bool LOTE_COBV_READ { get; set; }

        /*
        pix.write: Permissão para alteração de Pix

        pix.read: Permissão para consulta de Pix

        webhook.read: Permissão para consulta do webhook

        webhook.write: Permissão para alteração do webhook

        payloadlocation.write: Permissão para alteração de payloads

        payloadlocation.read: Permissão para consulta de payloads
        */
        /// <summary>
        /// Permissão para alteração de Pix
        /// </summary>
        public bool PIX_WRITE { get; set; }
        /// <summary>
        /// Permissão para consulta de Pix
        /// </summary>
        public bool PIX_READ { get; set; }

        /// <summary>
        /// Permissão para alteração do webhook
        /// </summary>
        public bool WEBHOOK_WRITE { get; set; }
        /// <summary>
        /// Permissão para consulta do webhook
        /// </summary>
        public bool WEBHOOK_READ { get; set; }

        /// <summary>
        /// Permissão para alteração de payloads
        /// </summary>
        public bool PAYLOAD_LOCATION_WRITE { get; set; }
        /// <summary>
        /// Permissão para consulta de payloads
        /// </summary>
        public bool PAYLOAD_LOCATION_READ { get; set; }

        public string[] ToScope()
        {
            List<string> lst = new List<string>();

            if (COB_WRITE) lst.Add("cob.write");
            if (COB_READ) lst.Add("cob.read");

            if (COBV_WRITE) lst.Add("cobv.write");
            if (COBV_READ) lst.Add("cobv.read");

            if (LOTE_COBV_WRITE) lst.Add("lotecobv.write");
            if (LOTE_COBV_READ) lst.Add("lotecobv.read");

            if (PIX_WRITE) lst.Add("pix.write");
            if (PIX_READ) lst.Add("pix.read");

            if (WEBHOOK_WRITE) lst.Add("webhook.write");
            if (WEBHOOK_READ) lst.Add("webhook.read");

            if (PAYLOAD_LOCATION_WRITE) lst.Add("payloadlocation.write");
            if (PAYLOAD_LOCATION_READ) lst.Add("payloadlocation.read");

            return lst.ToArray();
        }
        public string ToScopeString()
        {
            return string.Join(' ', ToScope());
        }
    }
}
