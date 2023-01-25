using System.Collections.Generic;

namespace Sicoob.Shared.Models
{
    public class Configuracao
    {
        /// <summary>
        /// Url do serviço OpenId
        /// </summary>
        public string UrlAutenticacao { get; set; } = "https://auth.sicoob.com.br/auth/realms/cooperado/protocol/openid-connect/";

        /// <summary>
        /// Id do cliente gerado pelo Sicoob no momento do cadastro do Aplicativo
        /// </summary>
        public string? ClientId { get; set; }
        /// <summary>
        /// Caminho do certificado com chave pública
        /// </summary>
        public string? UrlCertificadoPFX { get; set; }
        /// <summary>
        /// Senha do arquivo PFX, será apagada no construtor da classe `Sicoob`
        /// </summary>
        public string? CertificadoSenha { get; set; }
        /// <summary>
        /// Scopo de autorização, depende do serviço a ser utilizado
        /// </summary>
        public AuthorizationScope Scope { get; set; } = new AuthorizationScope();
    }
    public class ConfiguracaoAPI : Configuracao
    {
        /// <summary>
        /// Url do serviço de API
        /// </summary>
        public string UrlApi { get; set; } = "https://api.sicoob.com.br/";
    }

    public class AuthorizationScope
    {
        /* API PIX */
        /// <summary>
        /// [API PIX] cob.write: Permissão para alteração de cobranças imediatas
        /// </summary>
        public bool COB_WRITE { get; set; }
        /// <summary>
        /// [API PIX] cob.read: Permissão para consulta de cobranças imediatas
        /// </summary>
        public bool COB_READ { get; set; }
        /// <summary>
        /// [API PIX] cobv.write: Permissão para alteração de cobranças com vencimento
        /// </summary>
        public bool COBV_WRITE { get; set; }
        /// <summary>
        /// [API PIX] cobv.read: Permissão para consulta de cobranças com vencimento
        /// </summary>
        public bool COBV_READ { get; set; }
        /// <summary>
        /// [API PIX] lotecobv.write: Permissão para alteração de lotes de cobranças com vencimento
        /// </summary>
        public bool LOTE_COBV_WRITE { get; set; }
        /// <summary>
        /// [API PIX] lotecobv.read: Permissão para consulta de lotes de cobranças com vencimento
        /// </summary>
        public bool LOTE_COBV_READ { get; set; }

        /// <summary>
        /// [API PIX] pix.write: Permissão para alteração de Pix
        /// </summary>
        public bool PIX_WRITE { get; set; }
        /// <summary>
        /// [API PIX] pix.read: Permissão para consulta de Pix
        /// </summary>
        public bool PIX_READ { get; set; }

        /// <summary>
        /// [API PIX] webhook.read: Permissão para alteração do webhook
        /// </summary>
        public bool WEBHOOK_WRITE { get; set; }
        /// <summary>
        /// [API PIX] webhook.write: Permissão para consulta do webhook
        /// </summary>
        public bool WEBHOOK_READ { get; set; }

        /// <summary>
        /// [API PIX] payloadlocation.write: Permissão para alteração de payloads
        /// </summary>
        public bool PAYLOAD_LOCATION_WRITE { get; set; }
        /// <summary>
        /// [API PIX] payloadlocation.read: Permissão para consulta de payloads
        /// </summary>
        public bool PAYLOAD_LOCATION_READ { get; set; }

        /* API Conta Corrente */
        /// <summary>
        /// [API Conta Corrente] openid: Escopo de acesso para Logon para Conta Corrente
        /// </summary>
        public bool OPENID { get; set; }
        /// <summary>
        /// [API Conta Corrente] cco_extrato: Acessa dados do Extrato
        /// </summary>
        public bool CCO_EXTRATO { get; set; }
        /// <summary>
        /// [API Conta Corrente] cco_saldo: Acessa dados do Saldo
        /// </summary>
        public bool CCO_SALDO { get; set; }

        /* API Poupança */
        /// <summary>
        /// [API Conta Poupança] poupanca_contas: Acessa dados das Contas
        /// </summary>
        public bool POUPANCA_CONTAS { get; set; }
        /// <summary>
        /// [API Conta Poupança] poupanca_extrato: Acessa dados do Extrato
        /// </summary>
        public bool POUPANCA_EXTRATO { get; set; }
        /// <summary>
        /// [API Conta Poupança] poupanca_saldo: Acessa dados do Saldo
        /// </summary>
        public bool POUPANCA_SALDO { get; set; }
        
        /// <summary>
        /// Gera a lista de Scope utilizado na geração do Token
        /// </summary>
        public string[] ToScope()
        {
            List<string> lst = new List<string>();

            /* API PIX */
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

            /* API Conta Corrente */
            if (OPENID) lst.Add("openid");
            if (CCO_EXTRATO) lst.Add("cco_extrato");
            if (CCO_SALDO) lst.Add("cco_saldo");

            /* API Poupança */
            if (POUPANCA_CONTAS) lst.Add("poupanca_contas");
            if (POUPANCA_EXTRATO) lst.Add("poupanca_extrato");
            if (POUPANCA_SALDO) lst.Add("poupanca_saldo");

            return lst.ToArray();
        }
        /// <summary>
        /// Gera string utilizada na geração do Token
        /// </summary>
        /// <returns></returns>
        public string ToScopeString()
        {
            return string.Join(' ', ToScope());
        }

        public AuthorizationScope PIX_SetarPIX(bool valor)
        {
            PIX_READ = valor;
            PIX_WRITE = valor;
            return this;
        }
        public AuthorizationScope PIX_SetarCOB(bool valor)
        {
            COB_READ = valor;
            COB_WRITE = valor;
            return this;
        }
        public AuthorizationScope PIX_SetarCOBV(bool valor)
        {
            COBV_READ = valor;
            COBV_WRITE = valor;
            return this;
        }
        public AuthorizationScope PIX_SetarLote(bool valor)
        {
            LOTE_COBV_READ = valor;
            LOTE_COBV_WRITE = valor;
            return this;
        }
        public AuthorizationScope PIX_SetarWebhook(bool valor)
        {
            WEBHOOK_READ = valor;
            WEBHOOK_WRITE = valor;
            return this;
        }
        public AuthorizationScope PIX_SetarPayload(bool valor)
        {
            PAYLOAD_LOCATION_READ = valor;
            PAYLOAD_LOCATION_WRITE = valor;
            return this;
        }
                                  
        public AuthorizationScope PIX_Setar_Write(bool valor)
        {
            COB_WRITE = valor;
            COBV_WRITE = valor;
            LOTE_COBV_WRITE = valor;

            PIX_WRITE = valor;
            WEBHOOK_WRITE = valor;
            PAYLOAD_LOCATION_WRITE = valor;
            return this;
        }
        public AuthorizationScope PIX_Setar_Read(bool valor)
        {
            COB_READ = valor;
            COBV_READ = valor;
            LOTE_COBV_READ = valor;
            PIX_READ = valor;
            WEBHOOK_READ = valor;
            PAYLOAD_LOCATION_READ = valor;
            return this;
        }

        public AuthorizationScope CCorrente_Setar(bool valor)
        {
            OPENID = valor;
            CCO_EXTRATO = valor;
            CCO_SALDO = valor;
            return this;
        }
        public AuthorizationScope CPoupanca_Setar(bool valor)
        {
            POUPANCA_CONTAS = valor;
            POUPANCA_EXTRATO = valor;
            POUPANCA_SALDO = valor;
            return this;
        }

        public AuthorizationScope RemoverTodos()
        {
            return setarTodosComo(false);
        }

        /// <summary>
        /// Seta todos independente de novas propriedades serem criadas
        /// </summary>
        private AuthorizationScope setarTodosComo(bool valor)
        {
            var t = typeof(AuthorizationScope);
            foreach (var p in t.GetProperties())
            {
                if (!p.CanRead) continue;
                if (!p.CanWrite) continue;
                if (p.PropertyType != typeof(bool)) continue;

                p.SetValue(this, valor);
            }
            return this;
        }

    }
}
