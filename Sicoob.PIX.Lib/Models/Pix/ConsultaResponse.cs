using Sicoob.Shared.Models.Geral;
using System;

namespace Sicoob.PIX.Lib.Models.Pix
{
    public class ConsultaResponse
    {
        public ResponseParametros parametros { get; set; }
        public PixResponse[] pix { get; set; }

    }
    public class PixResponse
    {
        public string endToEndId { get; set; }
        public string txid { get; set; }
        public decimal valor { get; set; }
        public string chave { get; set; }
        public DateTime horario { get; set; }
        public string nomePagador { get; set; }
        public Pagador pagador { get; set; }
        public Devolucao[] devolucoes { get; set; }

        public override string ToString()
        {
            return $"{horario:g} {valor:C2} {nomePagador}";
        }

        public class Devolucao
        {
            public string id { get; set; }
            public string rtrId { get; set; }
            public string valor { get; set; }
            public DevolucaoHorario horario { get; set; }
            public string status { get; set; } // EM_PROCESSAMENTO, DEVOLVIDO, NAO_REALIZADO
            public string motivo { get; set; }
        }
        public class DevolucaoHorario
        {
            public DateTime solicitacao { get; set; }
            public DateTime liquidacao { get; set; }
        }
        public class Pagador
        {
            public string nome { get; set; }
            public string? cnpj { get; set; }
            public string? cpf { get; set; }
        }
    }
}
