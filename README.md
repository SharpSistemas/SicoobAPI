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
    Scope = AuthorizationScope.ReadOnly(),
    CertificadoSenha = "SenhaCertificado",
    UrlCertificadoPFX = "caminho/do/pfx/com/chave/privada.pfx"
};

// cria objeto
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
~~~

