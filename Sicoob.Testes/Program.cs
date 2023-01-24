using Newtonsoft.Json;
using Sicoob.Shared.Models;
using Sicoob.Testes;
using System.IO;

// carrega do disco
var cfg = JsonConvert.DeserializeObject<ConfiguracaoAPI>(File.ReadAllText("config.json"));

// salva no disco
//File.WriteAllText("config.json", JsonConvert.SerializeObject(cfg));

//var cfg = new ConfiguracaoAPI()
//{
//    ClientId = "00000000-0000-0000-0000-000000000000", // Obtém no "Aplicativo" no developers.sicoob.com.br
//    Scope = AuthorizationScope.ReadOnly(),
//    CertificadoSenha = "SenhaCertificado",
//    UrlCertificadoPFX = "caminho/do/pfx/com/chave/privada.pfx"
//};

//await TestesApiPIX.Run(cfg);
await TestesApiConta.Run(cfg);

