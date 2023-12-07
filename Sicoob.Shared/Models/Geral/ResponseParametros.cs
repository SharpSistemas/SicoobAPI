/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Shared.Models.Geral;

using System;

public class ResponseParametros
{
    public DateTime inicio { get; set; }
    public DateTime fim { get; set; }
    public Paginacao? paginacao { get; set; }

    public class Paginacao
    {
        public int paginaAtual { get; set; }
        public int itensPorPagina { get; set; }
        public int quantidadeDePaginas { get; set; }
        public int quantidadeTotalDeItens { get; set; }
    }
}
