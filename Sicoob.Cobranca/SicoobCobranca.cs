/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Cobranca;

using Sicoob.Cobranca.Models;
using Sicoob.Shared.Models.Acesso;
using Simple.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public sealed class SicoobCobranca : Shared.Sicoob
{
    // Documentações
    // > APIs tipo "Swagger":
    //   https://developers.sicoob.com.br/#!/apis

    private ClientInfo clientApi;
    public Shared.Models.ConfiguracaoAPI ConfigApi { get; }

    public SicoobCobranca(Shared.Models.ConfiguracaoAPI configApi)
        : base(configApi)
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

    public async Task<ConsultaBoletoResponse> ConsultarBoletos(ConsultaBoletoRequest consulta)
    {
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaBoletoResponse>("/cobranca-bancaria/v2/boletos", consulta));
    }
    public async Task<ConsultaBoletoResponse> ConsultarBoletosPagador(ConsultaBoletosPagadorRequest consulta)
    {
        return await ExecutaChamadaAsync(() => clientApi.GetAsync<ConsultaBoletoResponse>("/cobranca-bancaria/v2/boletos", consulta));
    }


}
