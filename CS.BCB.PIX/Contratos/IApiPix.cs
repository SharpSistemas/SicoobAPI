using CS.BCB.PIX.Models;
using System.Threading.Tasks;

namespace CS.BCB.PIX.Contratos
{
    public interface IApiPix
    {
        Task SetupAsync();

        /* Cobrança */
        Task<CobrancaImediata> ConsultarCobrancaAsync(string transactionId, int? revisao = null);
        Task<CobrancaImediata> CriarCobrancaAsync(string transactionId, NovaCobrancaImediata cobranca);
        Task<CobrancaImediata> CriarCobrancaAsync(NovaCobrancaImediata cobranca);
        Task<ListagemCobrancaImediata> ListarCobrancasAsync(ConsultarCobrancaImediata consulta);
        Task<CobrancaImediata> RevisarCobrancaAsync(string transactionId, RevisarCobrancaImediata cobranca);
        Task<byte[]> ConsultarImagemCobrancaAsync(string transactionId, int? revisao = null, int? largura = null);

        /* Cobrança Vencimento */
        Task<CobrancaImediata> ConsultarCobrancaVencimentoAsync(string transactionId, int? revisao = null);
        Task<CobrancaImediata> CriarCobrancaVencimentoAsync(string transactionId, NovaCobrancaImediata cobranca);
        Task<ListagemCobrancaImediata> ListarCobrancasVencimentoAsync(ConsultarCobrancaImediata consulta);
        Task<CobrancaImediata> RevisarCobrancaVencimentoAsync(string transactionId, RevisarCobrancaImediata cobranca);

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
