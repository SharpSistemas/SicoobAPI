namespace Sicoob.Cobranca.Models.v3;

public class ConsultaFaixasNossoNumeroRequest
{
    public int numeroCliente  { get; set; }
    public int codigoModalidade { get; set; }
    public int quantidade  { get; set; }
    public int? numeroContratoCobranca { get; set; }
}