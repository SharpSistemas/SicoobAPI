/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace CS.BCB.PIX.Models
{
    public class ListagemCobrancaVencimento
    {
        public ResponseParametros parametros { get; set; }
        public CobrancaVencimento[] cobs { get; set; }
    }
    public class CobrancaVencimento
    {
        public CalendarioVencimento calendario { get; set; }
        public string txid { get; set; }
        public int revisao { get; set; }
        public DadosDevedor devedor { get; set; }
        public DadosDevedor recebedor { get; set; }
        public DadosLoc loc { get; set; }
        public string status { get; set; }
        public ValorVencimento valor { get; set; }
        public string pixCopiaECola { get; set; }
        public string chave { get; set; }

        public string? solicitacaoPagador { get; set; }
        public NomeValor[] infoAdicionais { get; set; }

    }
}
