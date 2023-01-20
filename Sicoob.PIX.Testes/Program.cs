using Newtonsoft.Json;
using Sicoob.PIX;
using Sicoob.PIX.Models;
using System.IO;

// carrega do disco
var cfg = JsonConvert.DeserializeObject<ConfiguracaoPIX>(File.ReadAllText("config.json"));

// salva no disco
//File.WriteAllText("config.json", JsonConvert.SerializeObject(cfg));

var sicoob = new SicoobPIX(cfg);
await sicoob.SetupAsync();

var pixPeriodo = await sicoob.ConsultarPIXAsync(new Sicoob.PIX.Models.Pix.ConsultaRequest()
{
    inicio = new System.DateTime(2023, 01, 01),
    fim = new System.DateTime(2023, 01, 21),
});

var pix1 = await sicoob.ConsultarPIXAsync(pixPeriodo.pix[^1].endToEndId);

