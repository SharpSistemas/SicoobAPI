using Sicoob.Conta.Models.Shared;

namespace Sicoob.Conta.Models.v4;

public class SaldoResponse : SaldoResponseBase
{
    public decimal SaldoBloqueado { get; set; }
}