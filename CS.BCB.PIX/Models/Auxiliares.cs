/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using Newtonsoft.Json;
using System;

namespace CS.BCB.PIX.Models
{
    public class NomeCpfCnpj
    {
        public string? nome { get; set; }
        public string? cnpj { get; set; }
        public string? cpf { get; set; }
    }
    public class NomeValor
    {
        public string? nome { get; set; }
        public string? valor { get; set; }
    }
    public class Calendario : CalendarioCriacao
    {
        public DateTime criacao { get; set; }
    }
    public class CalendarioCriacao
    {
        /// <summary>
        /// Tempo de vida da cobrança, especificado em segundos a partir da data de criação
        /// </summary>
        public int expiracao { get; set; }
    }
    public class DadosLoc : DadosLocCriacao
    {
        public string location { get; set; }
        public string tipoCob { get; set; } // cob, cobv
        public DateTime criacao { get; set; }
        public string brcode { get; set; }
    }
    public class DadosLocCriacao
    {
        public int id { get; set; }
    }
    public class Valor
    {
        // O valor precisa serializar como string
        // Não funcionou fazer o mapeamento
        // Estou deixando com duas propriedades por hora

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
