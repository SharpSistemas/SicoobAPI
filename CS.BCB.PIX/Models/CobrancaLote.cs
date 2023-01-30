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
        public ProblemaCobrancaVencimentoLote problema { get; set; }
        public DateTime criacao { get; set; }
    }
    public class ProblemaCobrancaVencimentoLote
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string CorrelationId { get; set;}
        public ViolacaoCobrancaLote[] Violacoes { get; set; }
    }
    public class ViolacaoCobrancaLote
    {
        public string Razao { get; set; }
        public string Propriedade { get; set; }
        public string Valor { get; set; }
    }
}
