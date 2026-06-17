using Sicoob.Cobranca.Models.Shared;

namespace Sicoob.Cobranca.Models.v2;

public class BaixarBoletoRequest : BoletoBase
{
    public string seuNumero { get; set; }
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