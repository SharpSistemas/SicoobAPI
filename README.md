
# APIs Sicoob
Repositório para comunicação via API com o banco Sicoob

Link da documentação oficial: https://developers.sicoob.com.br

Para comunicação com o Sicoob é necessário um "Cadastro de Aplicativo" em developers.sicoob.com.br. 
Para isso é necessário ter os dados bancarios da conta a ser automatizada e de um certificado ICP-Brasil em arquivo.

Cada conta bancária a ser automatizada deve ter um cadastro como Aplicaitvo

É necessário ter o certificado em dois formatos: 
* *.PFX (padrão quando um certificado A1 é adiquirido) com a senha e;
* *.CER (apenas a chave pública em formato Base-64)

Durante o cadastro será informado os dados da conta bancária sendo automatizada e o certificado no formato PEM. Será gerado um `ClientId`.

O Sicoob possui APIs para:
* PIX
* Cobrança Bancária (Boleto)
* Conta Corrente
* Conta Poupança

Badges:

[![.NET](https://github.com/SharpSistemas/SicoobAPI/actions/workflows/dotnet.yml/badge.svg)](https://github.com/SharpSistemas/SicoobAPI)

API PIX:
[![NuGet](https://buildstats.info/nuget/Sicoob.PIX)](https://www.nuget.org/packages/Sicoob.PIX)

API Cobrança: 
[![NuGet](https://buildstats.info/nuget/Sicoob.Cobranca)](https://www.nuget.org/packages/Sicoob.Cobranca)

API Conta Corrente e Poupança: 
[![NuGet](https://buildstats.info/nuget/Sicoob.Conta)](https://www.nuget.org/packages/Sicoob.Conta)

A documentação do Sicoob é falha, imcompleta e em alguns tópicos é completamente ausente.
O Gerente não tem acesso à documentação, o WhatsApp das APIs manda solicitar via e-mail, e o e-mails não são respondidos.
Farei o póssível para documentar aqui as funções não documentadas lá, porém sob o risco de errar alguma funcionalidade

## Sicoob.PIX

APIs do Sicoob para funções PIX

Funcionalidades da API:
* Consultar e Devolver PIX recebidos no período (com e sem txId)
* Criar, Consultar e revisar Pix-Cobrança imediato
* Criar, Consultar e revisar Pix-Cobrança com vencimento
* Criar, Consultar e revisar lotes de Pix-Cobrança com vencimento
* Criar, Consultar e revisar PayloadLocation de Pix (cria um acesso público para dados de uma cobrança)
* Criar, consultar e cancelar Webhook

### Exemplos de uso

Criação do objeto API

~~~C#
// Cria configuração
var cfg = new ConfiguracaoAPI()
{
	ClientId = "00000000-0000-0000-0000-000000000000", // Obtém no "Aplicativo" no developers.sicoob.com.br
	Scope =  AuthorizationScope.PIX_SomenteLeitura(),
	CertificadoSenha = "SenhaCertificado",
	UrlCertificadoPFX = "caminho/do/pfx/com/chave/privada.pfx"
};

// cria o objeto de comunicação com as APIs de PIX
var sicoob = new SicoobPIX(cfg);
// Inicializa o acesso das APIs
await sicoob.SetupAsync();

// A autenticação dura 300 segundos (5min) e é auto renovado pela biblioteca
// Cada chamada verifica e, se necessário, atualiza o token
// Chame AtualizarCredenciaisAsync() para renovar manualmente
// É possível consultar dados da vigência do Token nas propriedades:
//  * sicoob.ExpiresIn
//  * sicoob.ExpiresAtUTC
//  * sicoob.Expired
~~~

Funções Cobrança
~~~C#
var cobs = await sicoob.ListarCobrancasAsync(new Sicoob.PIX.Models.Cobranca.ConsultaRequest
{
    inicio = DateTime.UtcNow.Date.AddDays(-1),
    fim = DateTime.UtcNow.AddDays(1).Date,
});
~~~

Funções PIX
~~~C#
/* PIX */
var pixPeriodo = await sicoob.ListarPIXAsync(new Sicoob.PIX.Models.Pix.ConsultaRequest()
{
    inicio = DateTime.UtcNow.Date.AddDays(-1),
    fim = DateTime.UtcNow.AddDays(1).Date,
});
~~~

**Atenção:** O construtor da classe Sicoob (e todas suas derivações, como o SicoobPIX) apaga a propriedade `CertificadoSenha` do objeto de configuração

### Webhook

A documentação do Webhook é um dos casos aonde só há informações parciais

Existe a documentação dos endpoints para criar e gerenciar o cadastro de Webhooks, porém não há nenhuma documentação da requisição que será feita pelo Sicoob no endpoint.

Ao consultar o suporte do sicoob no setor de APIs, fomos informados que não há documentação e devemos trabalhar por tentativa e erro (veja abaixo), portanto a documentação pode estar incompleta ou errada.


Após registrar o Webhook, todo PIX recebido que tenha TransactionId será notificado através de uma chamada Webhook, 
aonde os servidores do Sicoob farão uma chamada REST na aplicação com informações sobre o PIX.

Exemplo:

É registrado um Webhook para a chave: "12.345.678/0001-00":
~~~ C#
await sicoob.CriarWebHook("12345678000100", "https://meu.site.com.br/api/");
~~~

O Sicoob vai fazer um POST para `ttps://meu.site.com.br/api/pix`, repare que é feito um POST para o endereço registrado adicionado de `/pix` no final.

Caso seja registrado como `https://meu.site.com.br/api/pix`, será feito um POST para `https://meu.site.com.br/api/pix/pix` (repare o duplo `pix`).

O POST não tem nenhum header especial, e contém um array de dados do PIX no payload conforme model: `Sicoob.PIX.Models.Webhook.WebhookPostRequest`

~~~C# 
// Exemplo de um Endpoint em ASP
[HttpPost]
[Route("cuidado com sua rota aqui/pix")] // não esquecer do /pix
public IActionResult Webhook_Pix([FromBody] Sicoob.PIX.Models.Webhook.WebhookPostRequest payload){
    // Processa o payload
    return Ok();
}
~~~

Dump de uma requisição do sicoob capturada
~~~ JSON
{
  "method": "POST",
  "uri": "/api/pix",
  "headers": {
    "Via": "1.1 wsap501.sicoob.com.br:80 (Cisco-WSA/12.0.3-007)",
    "X-Imforwards": "20",
    "Content-Length": "164",
    "User-Agent": "axios/0.21.4",
    "Content-Type": "application/json",
    "Accept": "application/json, text/plain, */*",
    "Connection": "close"
  },
  "body": "{\"pix\":[{\"endToEndId\":\"xxxxxxxxxxx\",\"txid\":\"xxxxxxxxxxx\",\"valor\":\"xxx.xx\",\"horario\":\"2020-01-01T00:00:00.000Z\",\"devolucoes\":[]}]}",
}
~~~


Segue o Contato com a equipe do suporte ao desenvolvedor do Sicoob em JAN/2023:
~~~
Nós: 
Eu estou lendo a documentação sobre o registro, consulta e edição de WebHook
Mas não achei o protocolo da chamada que o Sicoob vai fazer
Não achei COMO o sicoob vai bater no meu endpoint
Qual método? (POST/GET?), não tem se vai mandar parâmetros, headers, qual o payload ....
Como eu faço para saber o que vou receber?

Sicoob ([NOME REMOVIDO]):
Você será notificado na URL cadastrada do Webhook e poderá identificar pelos IPs do Webhook PIX do Sicoob.

Infelizmente não temos exemplos disponíveis, será necessário testar sua aplicação.
~~~
Protocolo do Atendimento: [REMOVIDO]

