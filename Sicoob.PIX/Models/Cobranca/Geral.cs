using System;

namespace Sicoob.PIX.Models.Cobranca
{
    public class CalendarioRequest
    {
        /// <summary>
        /// Tempo de vida da cobrança, especificado em segundos a partir da data de criação
        /// </summary>
        public int expiracao { get; set; }
    }
    public class CalendarioResponse
    {
        public DateTime criacao { get; set; }

        /// <summary>
        /// Tempo de vida da cobrança, especificado em segundos a partir da data de criação
        /// </summary>
        public int expiracao { get; set; }
    }
    public class LocRequest
    {
        public int id { get; set; }
    }
    public class LocResponse
    {
        public int id { get; set; }
        public string location { get; set; }
        public string tipoCob { get; set; } // cob, cobv
        public DateTime criacao { get; set; }
        public string brcode { get; set; }
    }

    public class Valor
    {
        public decimal original { get; set; }
        public int modalidadeAlteracao { get; set; }
    }

}
