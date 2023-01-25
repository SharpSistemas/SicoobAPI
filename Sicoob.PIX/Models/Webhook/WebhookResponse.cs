/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.PIX.Models.Webhook;

using Sicoob.Shared.Models.Geral;
using System;

public class WebhookListResponse
{
    public ResponseParametros parametros { get; set; }
    public WebhookResponse[] webhooks { get; set; }
}

public class WebhookResponse
{
    public string webhookUrl { get; set; }
    public string chave { get; set; }
    public DateTime criacao { get; set; }
}
