using Newtonsoft.Json;

namespace Sicoob.Cobranca.Models;

public abstract class BoletoBase
{
    public int numeroContrato { get; set; }
    public int modalidade { get; set; } = 1; // Simples com Registro
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public int? nossoNumero { get; set; }
}