/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Conta;

using Sicoob.Shared.Models.Acesso;
using Simple.API;
using System.Net.Http;
using System.Threading.Tasks;

public sealed class SicoobContaPoupanca : Shared.Sicoob
{
    // Documentações
    // > APIs tipo "Swagger":
    //   https://developers.sicoob.com.br/#!/apis

    private ClientInfo clientApi;
    public Shared.Models.ConfiguracaoAPI ConfigApi { get; }

    public SicoobContaPoupanca(Shared.Models.ConfiguracaoAPI configApi, System.Security.Cryptography.X509Certificates.X509Certificate2? certificado = null)
        : base(configApi, certificado)
    {
        ConfigApi = configApi;
    }

    protected override void setupClients(HttpClientHandler handler)
    {
        clientApi = new ClientInfo(ConfigApi.UrlApi, handler);
        clientApi.SetHeader("x-sicoob-clientid", ConfigApi.ClientId);
    }
    protected override void atualizaClients(TokenResponse token)
    {
        clientApi.SetAuthorizationBearer(token.access_token);
    }

    /// <summary>
    /// Pesquisa as contas de poupança que pertencem a um CPF ou CNPJ.
    /// </summary>
    public async Task<string> ListarContasAsync()
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<string>($"/poupanca/v1/contas"));
    /// <summary>
    /// Consulta o extrato de um mês e ano de uma conta poupança.
    /// </summary>
    public async Task<string> ObterSaldoAsync(string contaPoupanca)
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<string>($"/poupanca/v1/contas/{contaPoupanca}/saldo"));
    /// <summary>
    /// Consulta o saldo atual de uma conta poupança.
    /// </summary>
    public async Task<string> ObterExtratoAsync(int mes, int ano, string contaPoupanca)
        => await ExecutaChamadaAsync(() => clientApi.GetAsync<string>($"/poupanca/v1/contas/{contaPoupanca}/extrato/{mes}/{ano}"));

}
