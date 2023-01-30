using CS.BCB.PIX.Models;
using System.Threading.Tasks;

namespace CS.BCB.PIX.Contratos
{
    public interface IApiPix
    {
        Task SetupAsync();

        /* PIX */
        Task<ListagemPixRecebido> ListarPIXAsync(ConsultarPix consulta);
        Task<PixRecebido> ConsultarPIXAsync(string endToEndId);
        Task<PixDevolucao> SolicitarDevlucaoPixAsync(string endToEndId, string idDevolucao, decimal valor);
        Task<PixDevolucao> ConsultarDevlucaoPixAsync(string endToEndId, string idDevolucao);

    }
}
