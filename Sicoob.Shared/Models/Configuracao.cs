using System.Collections.Generic;

namespace Sicoob.Shared.Models
{
    public class Configuracao
    {
        public string? ClientId { get; set; }

        public string UrlAutenticacao { get; set; } = "https://auth.sicoob.com.br/auth/realms/cooperado/protocol/openid-connect/";
        public string? UrlCertificadoPFX { get; set; }
        public string? CertificadoSenha { get; set; }
        public AuthorizationScope Scope { get; set; } = new AuthorizationScope();

    }
    public class ConfiguracaoAPI : Configuracao
    {
        public string UrlApi { get; set; } = "https://api.sicoob.com.br/";
    }

    public class AuthorizationScope
    {
        /// <summary>
        /// cob.write: Permissão para alteração de cobranças imediatas
        /// </summary>
        public bool COB_WRITE { get; set; }
        /// <summary>
        /// cob.read: Permissão para consulta de cobranças imediatas
        /// </summary>
        public bool COB_READ { get; set; }
        /// <summary>
        /// cobv.write: Permissão para alteração de cobranças com vencimento
        /// </summary>
        public bool COBV_WRITE { get; set; }
        /// <summary>
        /// cobv.read: Permissão para consulta de cobranças com vencimento
        /// </summary>
        public bool COBV_READ { get; set; }
        /// <summary>
        /// lotecobv.write: Permissão para alteração de lotes de cobranças com vencimento
        /// </summary>
        public bool LOTE_COBV_WRITE { get; set; }
        /// <summary>
        /// lotecobv.read: Permissão para consulta de lotes de cobranças com vencimento
        /// </summary>
        public bool LOTE_COBV_READ { get; set; }

        /// <summary>
        /// pix.write: Permissão para alteração de Pix
        /// </summary>
        public bool PIX_WRITE { get; set; }
        /// <summary>
        /// pix.read: Permissão para consulta de Pix
        /// </summary>
        public bool PIX_READ { get; set; }

        /// <summary>
        /// webhook.read: Permissão para alteração do webhook
        /// </summary>
        public bool WEBHOOK_WRITE { get; set; }
        /// <summary>
        /// webhook.write: Permissão para consulta do webhook
        /// </summary>
        public bool WEBHOOK_READ { get; set; }

        /// <summary>
        /// payloadlocation.write: Permissão para alteração de payloads
        /// </summary>
        public bool PAYLOAD_LOCATION_WRITE { get; set; }
        /// <summary>
        /// payloadlocation.read: Permissão para consulta de payloads
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

        public AuthorizationScope SetarPIX(bool valor)
        {
            PIX_READ = valor;
            PIX_WRITE = valor;
            return this;
        }
        public AuthorizationScope SetarCOB(bool valor)
        {
            COB_READ = valor;
            COB_WRITE = valor;
            return this;
        }
        public AuthorizationScope SetarCOBV(bool valor)
        {
            COBV_READ = valor;
            COBV_WRITE = valor;
            return this;
        }
        public AuthorizationScope SetarLote(bool valor)
        {
            LOTE_COBV_READ = valor;
            LOTE_COBV_WRITE = valor;
            return this;
        }
        public AuthorizationScope SetarWebhook(bool valor)
        {
            WEBHOOK_READ = valor;
            WEBHOOK_WRITE = valor;
            return this;
        }
        public AuthorizationScope SetarPayload(bool valor)
        {
            PAYLOAD_LOCATION_READ = valor;
            PAYLOAD_LOCATION_WRITE = valor;
            return this;
        }

        public AuthorizationScope Setar_Write(bool valor)
        {
            COB_WRITE = valor;
            COBV_WRITE = valor;
            LOTE_COBV_WRITE = valor;

            PIX_WRITE = valor;
            WEBHOOK_WRITE = valor;
            PAYLOAD_LOCATION_WRITE = valor;
            return this;
        }
        public AuthorizationScope Setar_Read(bool valor)
        {
            COB_READ = valor;
            COBV_READ = valor;
            LOTE_COBV_READ = valor;
            PIX_READ = valor;
            WEBHOOK_READ = valor;
            PAYLOAD_LOCATION_READ = valor;
            return this;
        }

        public static AuthorizationScope Nenhum()
        {
            return setarTodosComo(false);
        }
        public static AuthorizationScope Todos()
        {
            return setarTodosComo(true);
        }
        public static AuthorizationScope ReadOnly()
        {
            return setarTodosComo(false).Setar_Read(true);
        }

        /// <summary>
        /// Seta todos independente de novas propriedades serem criadas
        /// </summary>
        private static AuthorizationScope setarTodosComo(bool valor)
        {
            var obj = new AuthorizationScope();
            var t = typeof(AuthorizationScope);
            foreach (var p in t.GetProperties())
            {
                if (!p.CanRead) continue;
                if (!p.CanWrite) continue;
                if (p.PropertyType != typeof(bool)) continue;

                p.SetValue(obj, valor);
            }
            return obj;
        }

    }
}
