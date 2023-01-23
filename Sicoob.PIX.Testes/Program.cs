using Newtonsoft.Json;
using Sicoob.PIX;
using Sicoob.Shared.Models;
using System;
using System.IO;

// carrega do disco
var cfg = JsonConvert.DeserializeObject<ConfiguracaoAPI>(File.ReadAllText("config.json"));

// salva no disco
//File.WriteAllText("config.json", JsonConvert.SerializeObject(cfg));

var sicoob = new SicoobPIX(cfg);
await sicoob.SetupAsync();

/* COB */
var cobs = await sicoob.ListarCobrancasAsync(new Sicoob.PIX.Models.Cobranca.ConsultaRequest
{
    inicio = DateTime.UtcNow.Date.AddDays(-1),
    fim = DateTime.UtcNow.AddDays(1).Date,
});

/* PIX */
var pixPeriodo = await sicoob.ListarPIXAsync(new Sicoob.PIX.Models.Pix.ConsultaRequest()
{
    inicio = DateTime.UtcNow.Date.AddDays(-1),
    fim = DateTime.UtcNow.AddDays(1).Date,
});
var ultimoPix = await sicoob.ConsultarPIXAsync(pixPeriodo.pix[^1].endToEndId);



