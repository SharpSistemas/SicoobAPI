/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;

namespace CS.BCB.PIX.Models
{
    public class ConsultarCobranca : Consulta
    {
        public bool? locationPresente { get; set; }
        public string? status { get; set; }
    }
}
