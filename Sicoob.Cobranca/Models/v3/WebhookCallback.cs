using System;

namespace Sicoob.Cobranca.Models.v3;

public class WebhookCallback
{
    public int idWebhook { get; set; }
    public int tipoMovimento { get; set; }
    public WebhookDadosCallback dados { get; set; }
}

public class WebhookDadosCallback
{
    public string numeroIdentificadorBaixa { get; set; }
    public string codigoBarrasBoleto { get; set; }
    public string codigoBarrasBaixa { get; set; }
    public string nossoNumero { get; set; }
    public string seuNumero { get; set; }   
    public string codigoBancoRecebedor { get; set; }
    public int codigoAgenciaRecebedora { get; set; }
    public int numeroCliente { get; set; }
    public string cpfCnpjBeneficiario { get; set; }
    public string codigoTipoPessoaPagador { get; set; }
    public string cpfCnpjPagador { get; set; }
    public string nomePagador { get; set; }
    public string codigoTipoPessoaPortador { get; set; }
    public string cpfCnpjPortador { get; set; }
    public string nomePortador { get; set; }
    public decimal valorBoleto { get; set; }
    public decimal valorPagamento { get; set; }
    public int codigoCanalPagamento { get; set; }
    public DateTime dataEmissao { get; set; }
    public DateTime dataVencimento { get; set; }
    public DateTime? dataLimitePagamento { get; set; }   
    public DateTime dataHoraSituacaoBaixa { get; set; }
    public bool baixaRealizadaEmContingencia { get; set; }
    public bool cancelamentoBaixa { get; set; }
}