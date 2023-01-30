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
