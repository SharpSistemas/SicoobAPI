using Sicoob.Shared.Models.Geral;

namespace Sicoob.PIX.Models.Cobranca
{
    public class CobrancaCompleta
    {
        public CalendarioResponse calendario { get; set; }
        public NomeCpfCnpj devedor { get; set; }
        public LocResponse loc { get; set; }
        public Valor valor { get; set; }
        public string chave { get; set; }
        public string solicitacaoPagador { get; set; }
        public NomeValor[] infoAdicionais { get; set; }
        public string txid { get; set; }
        public int revisao { get; set; }
        public string location { get; set; }
        /// <summary>
        /// ATIVA, CONCLUIDA, REMOVIDA_PELO_USUARIO_RECEBEDOR, REMOVIDA_PELO_PSP, ATIVA, CONCLUIDA, REMOVIDA_PELO_USUARIO_RECEBEDOR, REMOVIDA_PELO_PSP
        /// </summary>
        public string status { get; set; }
        public string brcode { get; set; }
        public Pix.PixResponse[] pix { get; set; }
    }
}
