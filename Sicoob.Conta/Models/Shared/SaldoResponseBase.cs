/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Conta.Models.Shared;

public abstract class SaldoResponseBase
{
    public decimal Saldo { get; set; }
    public decimal SaldoLimite { get; set; }
}
