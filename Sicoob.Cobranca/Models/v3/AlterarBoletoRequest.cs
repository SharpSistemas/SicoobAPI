using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sicoob.Cobranca.Models.v3;

public class AlterarBoletoRequest
{
    public long numeroCliente { get; set; }
    public int codigoModalidade { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public EspecieDocumento? especieDocumento { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public SeuNumero? seuNumero { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public Desconto? desconto { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public Abatimento? abatimento { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public Multa? multa { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public JurosMora? jurosMora { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public RateioCredito? rateioCredito { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public Pix? pix { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public ProrrogacaoVencimento? prorrogacaoVencimento { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public ProrrogacaoLimitePagamento? prorrogacaoLimitePagamento { get; set; }
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public ValorNominal? valorNominal { get; set; }
}

public class EspecieDocumento
{
    public string codigoEspecieDocumento { get; set; }
}

public class SeuNumero
{
    public string seuNumero { get; set; }
    public string identificacaoBoletoEmpresa { get; set; }
}

public class Desconto
{
    public int tipoDesconto { get; set; }
    public DateTime dataPrimeiroDesconto { get; set; }
    public decimal valorPrimeiroDesconto { get; set; }
    public DateTime? dataSegundoDesconto { get; set; }
    public decimal? valorSegundoDesconto { get; set; }
    public DateTime? dataTerceiroDesconto { get; set; }
    public decimal? valorTerceiroDesconto { get; set; }
}

public class Abatimento
{
    public decimal valorAbatimento { get; set; }
}

public class Multa
{
    public int tipoMulta { get; set; }
    public DateTime dataMulta { get; set; }
    public decimal valorMulta { get; set; }
}

public class JurosMora
{
    public int tipoJurosMora { get; set; }
    public DateTime dataJurosMora { get; set; }
    public decimal valorJurosMora { get; set; }
}

public class RateioCredito
{
    public int tipoOperacao { get; set; }
    public List<RateioCreditoDetalhe> rateioCreditos { get; set; }
}

public class RateioCreditoDetalhe
{
    public int numeroBanco { get; set; }
    public int numeroAgencia { get; set; }
    public int numeroContaCorrente { get; set; }
    public bool contaPrincipal { get; set; }
    public int codigoTipoValorRateio { get; set; }
    public decimal valorRateio { get; set; }
    public int codigoTipoCalculoRateio { get; set; }
    public string numeroCpfCnpjTitular { get; set; }
    public string nomeTitular { get; set; }
    public int codigoFinalidadeTed { get; set; }
    public string codigoTipoContaDestinoTed { get; set; }
    public int quantidadeDiasFloat { get; set; }
    public DateTime dataFloatCredito { get; set; }
}

public class Pix
{
    public bool utilizarPix { get; set; }
}

public class ProrrogacaoVencimento
{
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime dataVencimento { get; set; }
}

public class ProrrogacaoLimitePagamento
{
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime dataLimitePagamento { get; set; }
}

public class ValorNominal
{
    public decimal valor { get; set; }
}