# Pix-Sicoob
Repositório para PIX no Sicoob

~~~C#
// Cria configuração
var cfg = new ConfiguracaoPIX()
{
    ClientId  = "00000000-0000-0000-0000-000000000000", // Obtém no "Aplicativo" no developers.sicoob.com.br
    Scope = new Sicoob.Shared.Models.AuthorizationScope()
    {
         PIX_READ = true,
    },
    CertificadoSenha = "SenhaCertificado",
    UrlCertificadoPFX = "caminho/do/pfx/com/chave/privada.pfx"
};
// cria objeto
var sicoob = new SicoobPIX(cfg);
// faz logon
await sicoob.SetupAsync(); 
// executa consultas
var pixPeriodo = await sicoob.ConsultarPIXAsync(new Sicoob.PIX.Lib.Models.Pix.ConsultaRequest()
{
    inicio = new System.DateTime(2020, 01, 01),
    fim = new System.DateTime(2020, 01, 31),
});

~~~

