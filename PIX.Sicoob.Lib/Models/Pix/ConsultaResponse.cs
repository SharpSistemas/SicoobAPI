using System;

namespace PIX.Sicoob.Lib.Models.Pix
{
    public class ConsultaResponse
    {
        public Parametros parametros { get; set; }
        public PixResponse[] pix { get; set; }


        public class Parametros
        {
            public DateTime inicio { get; set; }
            public DateTime fim { get; set; }
            public Paginacao paginacao { get; set; }
        }

        public class Paginacao
        {
            public int paginaAtual { get; set; }
            public int itensPorPagina { get; set; }
            public int quantidadeDePaginas { get; set; }
            public int quantidadeTotalDeItens { get; set; }
        }
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
