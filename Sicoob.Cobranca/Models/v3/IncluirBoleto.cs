using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sicoob.Cobranca.Models.Shared;

namespace Sicoob.Cobranca.Models.v3;

public class IncluirBoletoRequest
{
    public long numeroCliente { get; set; }
    public int codigoModalidade { get; set; }
    public int numeroContaCorrente { get; set; }
    public string codigoEspecieDocumento { get; set; } = "DM"; // Duplicata Mercantil, ver lista
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime? dataEmissao { get; set; }
    public long nossoNumero { get; set; }
    public string seuNumero { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public string? identificacaoBoletoEmpresa { get; set; }
    public int identificacaoEmissaoBoleto { get; set; }
    public int identificacaoDistribuicaoBoleto { get; set; }
    public decimal valor { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime dataVencimento { get; set; }
    [JsonConverter(typeof(CustomDateTimeConverter))]
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public DateTime? dataLimitePagamento { get; set; }
    public decimal valorAbatimento { get; set; }
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
    public string[] mensagensInstrucao { get; set; }
    public bool gerarPdf { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public Rateiocredito[] rateioCreditos { get; set; }
    /// <summary>
    /// 0: Padrão, 1: Com PIX, 2: Sem PIX
    /// </summary>
    public int codigoCadastrarPIX { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public int? numeroContratoCobranca { get; set; }
}