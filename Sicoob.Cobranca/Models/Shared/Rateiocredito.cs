namespace Sicoob.Cobranca.Models.Shared;

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
