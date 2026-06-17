using System;
using Newtonsoft.Json;
using Sicoob.Cobranca.Models.Shared;

namespace Sicoob.Cobranca.Models.v2;

public class AlterarDataVencimentoRequest : BoletoBase
{
    [JsonConverter(typeof(CustomDateTimeWithZoneConverter))]
    public DateTime dataVencimento { get; set; }
}

public class  AlterarDataVencimentoResponse
{
    public DadosDataVencimento[]? resultado { get; set; }
    public ResultadoInfo?[] mensagens { get; set; }
}
public class DadosDataVencimento
{
    public ResultadoInfo? status { get; set; }
    public AlterarDataVencimentoRequest? boleto { get; set; }
}