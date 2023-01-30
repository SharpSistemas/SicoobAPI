/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;

namespace CS.BCB.PIX.Models
{
    public class ConsultarPix : Consulta
    {
        public string? txid { get; set; }
        public bool? txIdPresente { get; set; }
        public bool? devolucaoPresente { get; set; }
    }
}
