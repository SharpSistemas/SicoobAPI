using Newtonsoft.Json;
using Sicoob.Conta;
using Sicoob.Shared.Models;
using System.IO;
using System.Threading.Tasks;

namespace Sicoob.Testes;

public static class TestesApiConta
{
    public static async Task Run_ContaCorrente()
    {
        // carrega do disco
        var cfg = JsonConvert.DeserializeObject<ConfiguracaoAPI>(File.ReadAllText("config_CC.json"));
        // salva no disco
        //var cfg = new ConfiguracaoAPI()
        //{
        //    ClientId = "00000000-0000-0000-0000-000000000000", // Obtém no "Aplicativo" no developers.sicoob.com.br
        //    CertificadoSenha = "SenhaCertificado",
        //    UrlCertificadoPFX = "caminho/do/pfx/com/chave/privada.pfx"
        //};
        //File.WriteAllText("config_CC.json", JsonConvert.SerializeObject(cfg));

        var cCorrente = new SicoobContaCorrente(cfg);
        await cCorrente.SetupAsync();

        var saldo = await cCorrente.ObterSaldoAsync();
        var extrato = await cCorrente.ObterExtratoAsync(12, 2022);
        extrato = extrato;
    }
    public static async Task Run_ContaPoupanca()
    {
        // carrega do disco
        var cfg = JsonConvert.DeserializeObject<ConfiguracaoAPI>(File.ReadAllText("config_CP.json"));
        // salva no disco
        //var cfg = new ConfiguracaoAPI()
        //{
        //    ClientId = "00000000-0000-0000-0000-000000000000", // Obtém no "Aplicativo" no developers.sicoob.com.br
        //    CertificadoSenha = "SenhaCertificado",
        //    UrlCertificadoPFX = "caminho/do/pfx/com/chave/privada.pfx"
        //};
        cfg.Scope.RemoverTodos().CPOUPANCA_Setar(true);
        File.WriteAllText("config_CP.json", JsonConvert.SerializeObject(cfg));

        var cPoupanca = new SicoobContaPoupanca(cfg);
        await cPoupanca.SetupAsync();

        var lista = await cPoupanca.ListarContasAsync();
        var extrato = await cPoupanca.ObterExtratoAsync(1, 2020, "00000");
        var saldo = await cPoupanca.ObterSaldoAsync("00000");
    }
}
