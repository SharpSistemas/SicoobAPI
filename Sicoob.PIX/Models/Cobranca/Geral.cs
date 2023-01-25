/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using Newtonsoft.Json;
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
        [JsonIgnore]
        public decimal original { get; set; }

        [JsonProperty(PropertyName = "original")]
        public string valor_para_serializacao
        {
            get { return original.ToString(System.Globalization.CultureInfo.InvariantCulture); }
            set { original = decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture); }
        }

        public int modalidadeAlteracao { get; set; }
    }

}
