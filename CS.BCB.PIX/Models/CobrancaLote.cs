/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;

namespace CS.BCB.PIX.Models
{
    public class NovaCobrancaVencimentoLote : RevisarCobrancaVencimentoLote
    {
        public string descricao { get; set; }
    }
    public class RevisarCobrancaVencimentoLote
    {
        public NovaCobrancaVencimento[] cobsv { get; set; }
    }
    public class ConsultarCobrancaVencimentoLote
    {
        public long id { get; set; }
        public string descricao { get; set; }
        public DateTime criacao { get; set; }
        public DadosCobrancaVencimentoLote[] cobsv { get; set; }
    }
    public class ConsultaLotesCobranca
    {
        public ResponseParametros parametros { get; set; }
        public ConsultarCobrancaVencimentoLote lotes { get; set; }
    }

    public class DadosCobrancaVencimentoLote
    {
        public string txid { get; set; }
        public string status { get; set; } // EM_PROCESSAMENTO, CRIADA, NEGADA
        public ErroRequisicao problema { get; set; }
        public DateTime criacao { get; set; }
    }
}
