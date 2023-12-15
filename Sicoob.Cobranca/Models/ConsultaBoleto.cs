namespace Sicoob.Cobranca.Models;

using System;

public class ConsultaBoletoRequest
{
    public int numeroContrato { get; set; }
    public int modalidade { get; set; } = 1; // Só tem a opção de `1`
    public int? nossoNumero { get; set; }
    public string? linhaDigitavel { get; set; }
    public string? codigoBarras { get; set; }
    public bool? gerarPdf { get; set; }
}
public class ConsultaBoletosPagadorRequest
{
    public int numeroContrato { get; set; }
    /// <summary>
    /// 1: Em Aberto
    /// 2: Baixado
    /// 3: Liquidado
    /// </summary>
    public int? codigoSituacao { get; set; }

    public string? dataInicio { get; set; }
    public string? dataFim { get; set; }
}

public class ConsultaBoletoResponse
{
    public DadosBoleto? resultado { get; set; }
}
public class ConsultaBoletosPagadorResponse
{
    public DadosBoleto[]? resultado { get; set; }
}
public class DadosBoleto
{
    public int numeroContrato { get; set; }
    public int modalidade { get; set; }
    public int numeroContaCorrente { get; set; }
    public string? especieDocumento { get; set; }
    public DateTime dataEmissao { get; set; }
    public int nossoNumero { get; set; }
    public string? seuNumero { get; set; }
    public string? identificacaoBoletoEmpresa { get; set; }
    public string? codigoBarras { get; set; }
    public string? linhaDigitavel { get; set; }
    public int identificacaoEmissaoBoleto { get; set; }
    public int identificacaoDistribuicaoBoleto { get; set; }
    public decimal valor { get; set; }
    public DateTime dataVencimento { get; set; }
    public DateTime dataLimitePagamento { get; set; }
    public decimal valorAbatimento { get; set; }
    public int tipoDesconto { get; set; }
    public DateTime dataPrimeiroDesconto { get; set; }
    public decimal valorPrimeiroDesconto { get; set; }
    public DateTime dataSegundoDesconto { get; set; }
    public decimal valorSegundoDesconto { get; set; }
    public DateTime dataTerceiroDesconto { get; set; }
    public decimal valorTerceiroDesconto { get; set; }
    public int tipoMulta { get; set; }
    public DateTime dataMulta { get; set; }
    public decimal valorMulta { get; set; }
    public int tipoJurosMora { get; set; }
    public DateTime dataJurosMora { get; set; }
    public decimal valorJurosMora { get; set; }
    public int numeroParcela { get; set; }
    public bool aceite { get; set; }
    public int codigoNegativacao { get; set; }
    public int numeroDiasNegativacao { get; set; }
    public int codigoProtesto { get; set; }
    public int numeroDiasProtesto { get; set; }
    public int quantidadeDiasFloat { get; set; }
    public DadosPagador? pagador { get; set; }
    public Beneficiariofinal? beneficiarioFinal { get; set; }
    public Mensagensinstrucao? mensagensInstrucao { get; set; }
    public Listahistorico[]? listaHistorico { get; set; }
    public string? situacaoBoleto { get; set; }
    public Rateiocredito[]? rateioCreditos { get; set; }
    public string? pdfBoleto { get; set; }
    public string? qrCode { get; set; }
}

public class Beneficiariofinal
{
    public string? numeroCpfCnpj { get; set; }
    public string? nome { get; set; }
}

public class Mensagensinstrucao
{
    public int tipoInstrucao { get; set; }
    public string[] mensagens { get; set; }
}

public class Listahistorico
{
    public DateTime dataHistorico { get; set; }
    public string? tipoHistorico { get; set; }
    public string? descricaoHistorico { get; set; }
}

public class Rateiocredito
{
    public int numeroBanco { get; set; }
    public int numeroAgencia { get; set; }
    public int numeroContaCorrente { get; set; }
    public bool contaPrincipal { get; set; }
    public int codigoTipoValorRateio { get; set; }
    public decimal valorRateio { get; set; }
    public int codigoTipoCalculoRateio { get; set; }
    public string? numeroCpfCnpjTitular { get; set; }
    public string? nomeTitular { get; set; }
    public int codigoFinalidadeTed { get; set; }
    public string? codigoTipoContaDestinoTed { get; set; }
    public int quantidadeDiasFloat { get; set; }
    public string? dataFloatCredito { get; set; }
}
