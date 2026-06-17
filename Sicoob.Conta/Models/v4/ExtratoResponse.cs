namespace Sicoob.Conta.Models.v4;

public class ExtratoResponse
{
    public decimal SaldoAtual { get; set; }
    public decimal SaldoBloqueado { get; set; }
    public decimal SaldoLimite { get; set; }
    public decimal SaldoAnterior { get; set; }
    public decimal SaldoBloqueioJudicial { get; set; }
    public decimal SaldoBloqueioJudicialAnterior { get; set; }
    public Transacao[] Transacoes { get; set; }
}   