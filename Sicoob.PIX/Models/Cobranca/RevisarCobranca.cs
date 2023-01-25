/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using Sicoob.Shared.Models.Geral;

namespace Sicoob.PIX.Models.Cobranca
{
    public class RevisarCobrancaRequest
    {
        public CalendarioRequest calendario { get; set; }
        public NomeCpfCnpj devedor { get; set; }
        public LocRequest loc { get; set; } 
        public string status { get; set; } // REMOVIDA_PELO_USUARIO_RECEBEDOR 
        public Valor valor { get; set; }
        public string chave { get; set; }
        public string solicitacaoPagador { get; set; }
        public NomeValor[] infoAdicionais { get; set; }
    }

    public class RevisarCobrancaResponse
    {
        public CalendarioResponse calendario { get; set; }
        public string txid { get; set; }
        public int revisao { get; set; }
        public NomeCpfCnpj devedor { get; set; }
        public LocResponse loc { get; set; }
        public string location { get; set; }
        /// <summary>
        /// ATIVA, CONCLUIDA, REMOVIDA_PELO_USUARIO_RECEBEDOR, REMOVIDA_PELO_PSP
        /// </summary>
        public string status { get; set; }
        public Valor valor { get; set; }
        public string brcode { get; set; }
        public string chave { get; set; }
        public string solicitacaoPagador { get; set; }
        public NomeValor[] infoAdicionais { get; set; }

    }
}
