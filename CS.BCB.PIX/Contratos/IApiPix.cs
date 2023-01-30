/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
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
        Task<CobrancaVencimento> ConsultarCobrancaVencimentoAsync(string transactionId, int? revisao = null);
        Task<CobrancaVencimento> CriarCobrancaVencimentoAsync(string transactionId, NovaCobrancaVencimento cobranca);
        Task<ListagemCobrancaVencimento> ListarCobrancasVencimentoAsync(ConsultarCobrancaImediata consulta);
        Task<CobrancaVencimento> RevisarCobrancaVencimentoAsync(string transactionId, RevisarCobrancaVencimento cobranca);

        /* Lotes de Cobrança */
        Task CriarLoteCobrancaVencimentoAsync(string idLote, NovaCobrancaVencimentoLote lote);
        Task RevisarLoteCobrancaVencimentoAsync(string idLote, RevisarCobrancaVencimentoLote lote);
        Task<ConsultarCobrancaVencimentoLote> ConsultarLoteCobrancaVencimentoAsync(string transactionId);
        Task<ConsultaLotesCobranca> ListarLoteCobrancaVencimentoAsync(Consulta consulta);

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
