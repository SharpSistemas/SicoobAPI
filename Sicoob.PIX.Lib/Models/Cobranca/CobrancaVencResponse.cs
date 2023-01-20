using System;

namespace Sicoob.PIX.Lib.Models.Cobranca
{
    internal class CobrancaVencResponse
    {
        public Parametros parametros { get; set; }
        public Cob[] cobs { get; set; }
    }

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

    public class Cob
    {
        public string _ref { get; set; }
    }

}
