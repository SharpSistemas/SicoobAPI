/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Testes;

using Newtonsoft.Json;
using Sicoob.Cobranca;
using Sicoob.Shared.Models;
using System;
using System.IO;
using System.Threading.Tasks;

public static class TestesApiCobranca
{
    internal static async Task Run_Cobranca()
    {
        // carrega do disco
        var cfg = JsonConvert.DeserializeObject<ConfiguracaoAPI>(File.ReadAllText("config_Cob.json")) ?? throw new Exception();
        cfg.Scope.RemoverTodos();
        cfg.Scope.Cobranca_Setar(true);
        File.WriteAllText("config_Cob.json", JsonConvert.SerializeObject(cfg));

        var cobranca = new SicoobCobranca(cfg);
        await cobranca.SetupAsync();

        //var consulta = await cobranca.ConsultarBoletos("531413");
        var consulta = await cobranca.ConsultarBoletosPagador(new Cobranca.Models.ConsultaBoletosPagadorRequest()
        {
            client_id = cfg.ClientId,
            numeroContrato = "531413",
            numeroCpfCnpj = "45769189000209",
        });


        cfg = cfg;
    }
}