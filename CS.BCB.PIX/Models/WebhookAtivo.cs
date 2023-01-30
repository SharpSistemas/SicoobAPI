/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;

namespace CS.BCB.PIX.Models
{
    public class ListagemWebhookAtivo
    {
        public ResponseParametros parametros { get; set; }
        public WebhookAtivo[] webhooks { get; set; }
    }
    public class WebhookAtivo
    {
        public string webhookUrl { get; set; }
        public string chave { get; set; }
        public DateTime criacao { get; set; }
    }
}
