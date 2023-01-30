/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using CS.BCB.PIX.Models;
using System;

namespace CS.BCB.PIX.Excecoes
{
    public class ErroRequisicaoException : Exception
    {
        public ErroRequisicao DadosErro { get; }

        public ErroRequisicaoException(ErroRequisicao erro)
            : base(erro.Title)
        {
            DadosErro = erro;
        }
        public ErroRequisicaoException(ErroRequisicao erro, Exception innerException)
            : base(erro.Title, innerException)
        {
            DadosErro = erro;
        }
    }
}
