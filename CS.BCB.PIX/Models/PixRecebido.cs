/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;

namespace CS.BCB.PIX.Models
{
    public class ListagemPixRecebido
    {
        public ResponseParametros parametros { get; set; }
        public PixRecebido[] pix { get; set; }
    }
    public class PixRecebido
    {
        public string endToEndId { get; set; }
        public string txid { get; set; }
        public decimal valor { get; set; }
        public string chave { get; set; }
        public DateTime horario { get; set; }
        public string nomePagador { get; set; }
        public NomeCpfCnpj pagador { get; set; }
        public PixDevolucao[] devolucoes { get; set; }

        public override string ToString()
        {
            string dev = "";
            if (devolucoes != null && devolucoes.Length > 0)
            {
                dev = $" [Dev:{dev.Length}]";
            }

            return $"{horario:g} {valor:C2} {nomePagador}{dev}";
        }
    }
}
