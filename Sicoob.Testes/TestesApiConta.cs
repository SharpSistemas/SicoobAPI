using Sicoob.Conta;
using System.Threading.Tasks;

namespace Sicoob.Testes;

public static class TestesApiConta
{
    public static async Task Run(Shared.Models.ConfiguracaoAPI? cfg)
    {
        var cCorrente = new SicoobContaCorrente(cfg);

        var saldo = cCorrente.ObterSaldoAsync();


        //var cPoupanca = new SicoobContaPoupanca(cfg);

    }
}
