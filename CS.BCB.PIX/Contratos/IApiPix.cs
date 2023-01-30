using CS.BCB.PIX.Models;
using System.Threading.Tasks;

namespace CS.BCB.PIX.Contratos
{
    public interface IApiPix
    {
        Task SetupAsync();

        /* Cobrança */
        Task<Cobranca> ConsultarCobrancaAsync(string transactionId, int? revisao = null);
        Task<Cobranca> CriarCobrancaAsync(string transactionId, NovaCobranca cobranca);
        Task<Cobranca> CriarCobrancaAsync(NovaCobranca cobranca);
        Task<ListagemCobrancaRecebida> ListarCobrancasAsync(ConsultarCobranca consulta);
        Task<Cobranca> RevisarCobrancaAsync(string transactionId, RevisarCobranca cobranca);
        Task<byte[]> ConsultarImagemCobrancaAsync(string transactionId, int? revisao = null, int? largura = null);
        /* PIX */
        Task<ListagemPixRecebido> ListarPIXAsync(ConsultarPix consulta);
        Task<PixRecebido> ConsultarPIXAsync(string endToEndId);
        Task<PixDevolucao> SolicitarDevlucaoPixAsync(string endToEndId, string idDevolucao, decimal valor);
        Task<PixDevolucao> ConsultarDevlucaoPixAsync(string endToEndId, string idDevolucao);

        /* WebHook */
        Task CriarWebHookAsync(string chave, string url);
        Task<ListagemWebhookAtivo> ConsultarWebHooksAsync();
        Task<WebhookAtivo> ConsultarWebHookAsync(string chave);
        Task CancelarWebHookAsync(string chave);
    }
}
