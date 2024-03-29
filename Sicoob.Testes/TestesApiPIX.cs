/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Testes;

using Newtonsoft.Json;
using Sicoob.PIX;
using System;
using System.IO;
using System.Threading.Tasks;

public static class TestesApiPIX
{
    public static async Task Run()
    {
        // carrega do disco
        var cfg = JsonConvert.DeserializeObject<Shared.Models.ConfiguracaoAPI>(File.ReadAllText("config_PIX.json"));
        // salva no disco
        //File.WriteAllText("config_PIX.json", JsonConvert.SerializeObject(cfg));

        // Cria Objeto
        var sicoob = new SicoobPIX(cfg);
        // Configura acesso das APIs
        await sicoob.SetupAsync();
        // A autenticação dura 300 segundos (5min) e é auto renovado pela biblioteca
        // Cada chamada verifica e, se necessário, atualiza o token
        // Chame AtualizarCredenciaisAsync() para renovar manualmente
        // É possível consultar dados da vigência do Token nas propriedades:
        //  * sicoob.ExpiresIn
        //  * sicoob.ExpiresAtUTC
        //  * sicoob.Expired

        //var allHooks = await sicoob.ConsultarWebHooksAsync();
        //var chaveHooks = await sicoob.ConsultarWebHookAsync("17189722000139");

        //var novo = await sicoob.CriarCobrancaAsync("TESTC001F1AA20230203T092900", CS.BCB.PIX.Models.NovaCobrancaImediata.Padrao("17189722000139", 1.90M));

        /* COB */
        //var cobs = await sicoob.ListarCobrancasAsync(new CS.BCB.PIX.Models.ConsultarCobrancaImediata()
        //{
        //    inicio = DateTime.UtcNow.Date.AddDays(-7),
        //    fim = DateTime.UtcNow.AddDays(1).Date,
        //});

        /* PIX */
        //var pixPeriodo = await sicoob.ListarPIXAsync(new CS.BCB.PIX.Models.ConsultarPix()
        //{
        //    inicio = DateTime.UtcNow.Date.AddDays(-7),
        //    fim = DateTime.UtcNow.AddDays(1).Date,
        //    //paginacao = new CS.BCB.PIX.Models.ConsultaPaginacao()
        //    //{
        //    //    itensPorPagina = 50,
        //    //}
        //});
        //var ultimoPix = await sicoob.ConsultarPIXAsync(pixPeriodo.pix[^1].endToEndId);


    }
}
