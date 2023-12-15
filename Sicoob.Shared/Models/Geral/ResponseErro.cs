namespace Sicoob.Shared.Models.Geral;

using System;
using System.Linq;

public class ErroRequisicao
{
    public Mensagens[] mensagens { get; set; }

    public string ObterMensagemErro()
    {
        return string.Join("; ", mensagens.Select(m => m.mensagem));
    }

    public class Mensagens
    {
        public string mensagem { get; set; }
        public int codigo { get; set; }
    }
}
public class ErroRequisicaoException : Exception
{
    public ErroRequisicao DadosErro { get; }

    public ErroRequisicaoException(ErroRequisicao erro)
        : base(erro.ObterMensagemErro())
    {
        DadosErro = erro;
    }

    public ErroRequisicaoException(ErroRequisicao erro, Exception innerException)
        : base(erro.ObterMensagemErro(), innerException)
    {
        DadosErro = erro;
    }
}
