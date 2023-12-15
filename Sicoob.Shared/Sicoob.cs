/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Shared;

using Simple.API;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public abstract class Sicoob
{
    private readonly Models.Configuracao config;
    private readonly HttpClientHandler httpHandler;
    private ClientInfo clientAuth;

    public DateTime ExpiresAtUTC { get; private set; }
    public TimeSpan ExpiresIn => ExpiresAtUTC - DateTime.UtcNow;
    public bool Expired => ExpiresIn.TotalSeconds < 0;

    public Sicoob(Models.Configuracao config, X509Certificate2? certificado = null)
    {
        this.config = config ?? throw new ArgumentNullException(nameof(config));

        if (certificado == null)
        {
            certificado = new X509Certificate2(config.UrlCertificadoPFX, config.CertificadoSenha);
            // Após abrir o PFX, não manter a senha na memória
            // Não apenas setar em NULL, trocar a referência
            config.CertificadoSenha = "*******";
        }
        else
        {
            if (config.UrlCertificadoPFX != null) throw new ArgumentException("Não é possível informar o caminho do certificado pois o X509Certificate2 já foi informado");
            if (config.CertificadoSenha != null) throw new ArgumentException("Não é possível informar a senha do certificado pois o X509Certificate2 já foi informado");
        }

        httpHandler = new HttpClientHandler();
        httpHandler.ClientCertificates.Add(certificado);

        clientAuth = new ClientInfo(config.UrlAutenticacao, httpHandler);
    }

    protected void enableDebug(ClientInfo clientApi)
    {
        clientApi.SetHeader("apim-debug", "true"); // debug de OPEN-ID
        clientApi.BeforeSend += ClientApi_BeforeSend;
        clientApi.ResponseDataReceived += ClientApi_ResponseDataReceived;
        debugLog("[SETUP]", "STARTUP");
    }
    private void ClientApi_ResponseDataReceived(object sender, ClientInfo.ResponseReceived e)
    {
        debugLog("<<", $"[{e.StatusCode}] RECV: {e.Content}");
    }
    private void ClientApi_BeforeSend(object sender, HttpRequestMessage e)
    {
        string content = "";
        if (e.Content != null)
        {
            if (e.Content is StringContent strCnt)
            {
                content = strCnt.ReadAsStringAsync().Result;
            }
        }

        debugLog(">>", $"[{e.Method}] {e.RequestUri} {content}");
    }
    private void debugLog(string direciton, string content)
    {
        System.IO.File.AppendAllText("debug.log", $"{DateTime.Now:G} {direciton} {content}\r\n");
    }

    public async Task SetupAsync()
    {
        setupClients(httpHandler);
        await atualizaCredenciaisAsync();
    }
    protected abstract void setupClients(HttpClientHandler handler);
    protected abstract void atualizaClients(Models.Acesso.TokenResponse token);

    public async Task AtualizarCredenciaisAsync()
        => await atualizaCredenciaisAsync();
    private async Task atualizaCredenciaisAsync()
    {
        var response = await clientAuth.FormUrlEncodedPostAsync<Models.Acesso.TokenResponse>("token", new
        {
            client_id = config.ClientId,
            grant_type = "client_credentials",
            scope = config.Scope.ToScopeString(),
        });
        response.EnsureSuccessStatusCode();
        atualizaClients(response.Data);
        ExpiresAtUTC = DateTime.UtcNow.AddSeconds(response.Data.expires_in);
    }

    protected async Task<bool> VerificaAtualizaCredenciaisAsync()
    {
        if (ExpiresIn.TotalSeconds >= 5)
        {
            return false;
        }

        await atualizaCredenciaisAsync();
        return true;
    }
    protected async Task<T> ExecutaChamadaAsync<T>(Func<Task<Response<T>>> func)
    {
        await VerificaAtualizaCredenciaisAsync();
        Response<T> response = await func();

        if (!response.IsSuccessStatusCode)
        {
            if (response.TryParseErrorResponseData(out CS.BCB.PIX.Models.ErroRequisicao err))
            {
                throw new CS.BCB.PIX.Excecoes.ErroRequisicaoException(err);
            }
        }
        response.EnsureSuccessStatusCode();

        return response.Data;
    }
    protected async Task ExecutaChamadaAsync(Func<Task<Response>> func)
    {
        await VerificaAtualizaCredenciaisAsync();
        Response response = await func();
        
        // Processa manualmente para não envelopar demais
        if (response.IsSuccessStatusCode) return;
        if (response.TryParseErrorResponseData(out CS.BCB.PIX.Models.ErroRequisicao err))
        {
            throw new CS.BCB.PIX.Excecoes.ErroRequisicaoException(err);
        }
        // Se não era um ErroRequisição, usar o erro comum
        response.EnsureSuccessStatusCode();
    }
}
