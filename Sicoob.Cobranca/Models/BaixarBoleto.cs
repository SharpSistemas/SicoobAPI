namespace Sicoob.Cobranca.Models;

public class BaixarBoletoRequest : BoletoBase
{
    
}

public class BaixarBoletoResponse
{
    public DadosBaixa[]? resultado { get; set; }
    public ResultadoInfo? mensagens { get; set; }
}
public class DadosBaixa
{
    public ResultadoInfo? status { get; set; }
    public BaixarBoletoRequest? boleto { get; set; }
}