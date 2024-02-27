using Newtonsoft.Json;

namespace Sicoob.Cobranca.Models;

using System;

public class IncluirBoletosRequest : BoletoBase
{
    public int numeroContaCorrente { get; set; }
    public string especieDocumento { get; set; } = "DM"; // Duplicata Mercantil, ver lista
    
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime dataEmissao { get; set; }
    public string seuNumero { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public string identificacaoBoletoEmpresa { get; set; }
    public int identificacaoEmissaoBoleto { get; set; }
    public int identificacaoDistribuicaoBoleto { get; set; }
    public decimal valor { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime dataVencimento { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public DateTime? dataLimitePagamento { get; set; }
    public int valorAbatimento { get; set; }
    public int tipoDesconto { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public DateTime? dataPrimeiroDesconto { get; set; }
    public decimal valorPrimeiroDesconto { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public DateTime? dataSegundoDesconto { get; set; }
    public decimal valorSegundoDesconto { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public DateTime? dataTerceiroDesconto { get; set; }
    public decimal valorTerceiroDesconto { get; set; }
    public int tipoMulta { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime dataMulta { get; set; }
    public decimal valorMulta { get; set; }
    public int tipoJurosMora { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime dataJurosMora { get; set; }
    public decimal valorJurosMora { get; set; }
    public int numeroParcela { get; set; }
    public bool aceite { get; set; }
    public int codigoNegativacao { get; set; }
    public int numeroDiasNegativacao { get; set; }
    public int codigoProtesto { get; set; }
    public int numeroDiasProtesto { get; set; }
    public DadosPagador pagador { get; set; }
    public Beneficiariofinal beneficiarioFinal { get; set; }
    public Mensagensinstrucao mensagensInstrucao { get; set; }
    public bool gerarPdf { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public Rateiocredito?[] rateioCreditos { get; set; }
    /// <summary>
    /// 0: Padrão, 1: Com PIX, 2: Sem PIX
    /// </summary>
    public int codigoCadastrarPIX { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public int? numeroContratoCobranca { get; set; }
}

public class IncluirBoletosResponse
{
    public DadosInclusao[]? resultado { get; set; }
}
public class DadosInclusao
{
    public ResultadoInfo? status { get; set; }
    public DadosBoleto? boleto { get; set; }
}