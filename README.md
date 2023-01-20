# APIs Sicoob
Repositório para comunicação via API com o banco Sicoob

Link da documentação oficial: https://developers.sicoob.com.br

## PIX
Exemplo de uso
~~~C#
// Cria configuração
var cfg = new ConfiguracaoAPI()
{
    ClientId = "00000000-0000-0000-0000-000000000000", // Obtém no "Aplicativo" no developers.sicoob.com.br
    Scope = new AuthorizationScope()
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
var pixPeriodo = await sicoob.ConsultarPIXAsync(new ConsultaRequest()
{
    inicio = new System.DateTime(2020, 01, 01),
    fim = new System.DateTime(2020, 01, 31),
});
~~~

