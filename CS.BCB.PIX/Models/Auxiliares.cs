/**************************************\
 * Biblioteca C# para APIs do PIX     *
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
    public class CalendarioImediata : CalendarioImadiataCriacao
    {
        public DateTime criacao { get; set; }
    }
    public class CalendarioImadiataCriacao
    {
        /// <summary>
        /// Tempo de vida da cobrança, especificado em segundos a partir da data de criação
        /// </summary>
        public int expiracao { get; set; }
    }
    public class CalendarioVencimento : CalendarioVencimentoCriacao
    {
        public DateTime criacao { get; set; }
    }
    public class CalendarioVencimentoCriacao
    {
        public DateTime DataDeVencimento { get; set; }
        public int ValidadeAposVencimento { get; set; }
    }
    public class DadosLoc : DadosLocCriacao
    {
        public int id { get; set; }
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
    public class ValorVencimento : Valor
    {
        public ModalidadeValorPercentual Multa { get; set; }
        public ModalidadeValorPercentual Juros { get; set; }
        public ModalidadeValorPercentual Abatimento { get; set; }
        public DescontoCobrancaVencimento Desconto { get; set; }
    }
    public class ModalidadeValorPercentual : ValorPercentual
    {
        public int modalidade { get; set; }
    }
    public class DescontoCobrancaVencimento
    {
        public DescontoDataFixa descontoDataFixa { get; set; }
        public int modalidade { get; set; }
    }
    public class DescontoDataFixa : ValorPercentual
    {
        public DateTime data { get; set; }
    }
    public class ValorPercentual
    {
        // O valor precisa serializar como string
        // Não funcionou fazer o mapeamento
        // Estou deixando com duas propriedades por hora
        [JsonIgnore]
        public decimal valorPerc { get; set; }

        [JsonProperty(PropertyName = "valorPerc")]
        public string valor_para_serializacao
        {
            get { return valorPerc.ToString(System.Globalization.CultureInfo.InvariantCulture); }
            set { valorPerc = decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture); }
        }
    }

    public class ResponseParametros
    {
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }
        public Paginacao? paginacao { get; set; }

        public class Paginacao
        {
            public int paginaAtual { get; set; }
            public int itensPorPagina { get; set; }
            public int quantidadeDePaginas { get; set; }
            public int quantidadeTotalDeItens { get; set; }
        }
    }
    public class ConsultaPaginacao
    {
        /// <summary>
        /// Página a trazida
        /// </summary>
        public int paginaAtual { get; set; }
        /// <summary>
        /// Quantidade de produtos por página
        /// </summary>
        public int itensPorPagina { get; set; }
    }
    public class ConsultaCpfCnpj : Consulta
    {
        /// <summary>
        /// Indica CPF para fazer a consulta
        /// </summary>
        public string? cpf { get; set; }
        /// <summary>
        /// Indica CNPJ para fazer a consulta
        /// </summary>
        public string? cnpj { get; set; }
    }
    public class Consulta
    {
        /// <summary>
        /// Data inicial da busca
        /// </summary>
        public DateTime inicio { get; set; }
        /// <summary>
        /// Data final (inlusive) da busca
        /// </summary>
        public DateTime fim { get; set; }
        /// <summary>
        /// Informação sobre paginação de resultados
        /// </summary>
        public ConsultaPaginacao? paginacao { get; set; }
    }
}
