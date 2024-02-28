using System;
using Newtonsoft.Json;

namespace Sicoob.Cobranca.Models;

public class AlterarDataVencimentoRequest : BoletoBase
{
    [JsonConverter(typeof(CustomDateTimeConverter))]
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