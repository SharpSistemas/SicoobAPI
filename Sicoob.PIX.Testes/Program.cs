// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Sicoob.PIX.Lib;
using Sicoob.PIX.Lib.Models;
using System.IO;

var cfg = JsonConvert.DeserializeObject<ConfiguracaoAPI>(File.ReadAllText("config.json"));
// save (atualiza cfg)
//File.WriteAllText("config.json", JsonConvert.SerializeObject(cfg));

var sicoob = new SicoobAPI(cfg);
await sicoob.SetupAsync();

var pixPeriodo = await sicoob.ConsultarPIX(new Sicoob.PIX.Lib.Models.Pix.ConsultaRequest()
{
    inicio = new System.DateTime(2023, 01, 01),
    fim = new System.DateTime(2023, 01, 15),
});

var pix1 = await sicoob.ConsultarPIX(pixPeriodo.pix[0].endToEndId);

