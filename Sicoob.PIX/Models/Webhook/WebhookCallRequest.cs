/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;

namespace Sicoob.PIX.Models.Webhook
{
    public class WebhookPostRequest
    {
        public WebhookPixRequest[] pix { get; set; }
    }

    public class WebhookPixRequest
    {
        public string endToEndId { get; set; }
        public string txid { get; set; }
        public string valor { get; set; }
        public DateTime horario { get; set; }
        public WebhookPixDevolucaoRequest[] devolucoes { get; set; }
    }
    public class WebhookPixDevolucaoRequest
    {
        public string id { get; set; }
        public string rtrId { get; set; }
        public string valor { get; set; }
        public object horario { get; set; } // Não testado
        public string status { get; set; } 
        public string motivo { get; set; }
    }
}
