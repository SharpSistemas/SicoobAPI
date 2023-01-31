/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;

namespace CS.BCB.PIX.Models
{
    public class ConsultarPix : ConsultaCpfCnpj
    {
        /// <summary>
        /// Informa um id de transação a ser consultado
        /// </summary>
        public string? txid { get; set; }
        /// <summary>
        /// Filtra apenas transações com ou sem id de transação.
        /// NULL: Todos; true: Apenas com TxId; false: Apenas sem TxId
        /// </summary>
        public bool? txIdPresente { get; set; }
        /// <summary>
        /// Filtra apenas transações com ou sem devolução.
        /// NULL: Todos; true: Apenas com devoluções; false: Apenas sem devoluções
        /// </summary>
        public bool? devolucaoPresente { get; set; }
    }
}
