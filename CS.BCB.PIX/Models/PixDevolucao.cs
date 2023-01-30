/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;

namespace CS.BCB.PIX.Models
{
    public class PixDevolucao
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
}
