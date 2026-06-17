using Newtonsoft.Json;
using Sicoob.Cobranca.Models.Shared;

namespace Sicoob.Cobranca.Models.v2;

public class ProtestarBoletoRequest: BoletoBase
{
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public int? numeroContratoCobranca { get; set; }
}

public class ProtestarBoletoResponse
{
    public DadosProtestar[]? resultado { get; set; }
    public ResultadoInfo[]? mensagens { get; set; }
}
public class DadosProtestar
{
    public ResultadoInfo? status { get; set; }
    public ProtestarBoletoRequest? boleto { get; set; }
}