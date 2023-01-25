/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.PIX.Models.Pix;

using System;

public class DevolucaoResponse
{
    public enum ListaStatus
    {
        EM_PROCESSAMENTO,
        DEVOLVIDO,
        NAO_REALIZADO,

        DESCONHECIDO,
    }

    public string id { get; set; }
    public string rtrId { get; set; }
    public string valor { get; set; }
    public Horario horario { get; set; }
    /// <summary>
    /// EM_PROCESSAMENTO, DEVOLVIDO, NAO_REALIZADO
    /// </summary>
    public string status { get; set; }
    public string motivo { get; set; }

    public ListaStatus ObterStatus()
    {
        if (!Enum.TryParse(status, out ListaStatus result))
        {
            result = ListaStatus.DESCONHECIDO;
        }

        return result;
    }

    public class Horario
    {
        public DateTime solicitacao { get; set; }
        public DateTime liquidacao { get; set; }
    }
}
