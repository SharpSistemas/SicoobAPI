/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Testes;

using Newtonsoft.Json;
using Sicoob.Conta;
using Sicoob.Shared.Models;
using System.IO;
using System.Threading.Tasks;

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

        var cCorrente = new SicoobContaCorrente(cfg, 00000);
        await cCorrente.SetupAsync();

        var extrato = await cCorrente.ObterExtratoAsync(12, 2023);
        var saldo = await cCorrente.ObterSaldoAsync();
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
        //File.WriteAllText("config_CP.json", JsonConvert.SerializeObject(cfg));

        var cPoupanca = new SicoobContaPoupanca(cfg);
        await cPoupanca.SetupAsync();

        var lista = await cPoupanca.ListarContasAsync();
        var extrato = await cPoupanca.ObterExtratoAsync(1, 2020, "00000");
        var saldo = await cPoupanca.ObterSaldoAsync("00000");
    }
}
