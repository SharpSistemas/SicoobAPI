using System;

namespace PIX.Sicoob.Lib.Models.Pix
{
    public class ConsultaRequest
    {
        public DateTime inicio { get; set; }
        public DateTime fim { get; set; }
        public string? txid { get; set; }
        public bool? txIdPresente { get; set; }
        public bool? devolucaoPresente { get; set; }
        public string? cpf { get; set; }
        public string? cnpj { get; set; }

        public Paginacao? paginacao {get;set;}

    }
    public class Paginacao
    {
        public int paginaAtual { get; set; }
        public int itensPorPagina { get; set; }
    }
}
