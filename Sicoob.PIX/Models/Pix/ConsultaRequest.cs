using Sicoob.Shared.Models.Geral;
using System;

namespace Sicoob.PIX.Models.Pix
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

        public RequestPaginacao? paginacao {get;set;}

    }
}
