namespace Sicoob.Shared.Models.Geral;

using System;
using System.Linq;

public class ErroRequisicaoErrors : IErroRequisicao
{
    public Erros[]? errors { get; set; }
    public string ObterMensagemErroCode()
    {
        return errors is null
            ? string.Empty
            : string.Join("; ", errors.Select(m => m.codigo + " " + (m.title != m.detail ? m.title + " " + m.detail : m.detail)));
    }
    public string ObterMensagemErro()
    {
        return errors is null
            ? string.Empty
            : string.Join("; ", errors.Select(m => m.title != m.detail ? m.title + " " + m.detail : m.detail));
    }
    
    public class  Erros
    {
        public string title { get; set; }
        public string detail { get; set; }
        public int codigo { get; set; }
    }
}

public class ErroRequisicaoMensagens : IErroRequisicao
{
    public Mensagens[]? mensagens { get; set; }

    public string ObterMensagemErro()
    {
        return mensagens is null ? string.Empty : string.Join("; ", mensagens.Select(m => m.mensagem));
    }

    public class Mensagens
    {
        public string mensagem { get; set; }
        public int codigo { get; set; }
    }
}
public class ErroRequisicaoException : Exception
{
    public IErroRequisicao DadosErro { get; }

    public ErroRequisicaoException(IErroRequisicao erro)
        : base(erro.ObterMensagemErro())
    {
        DadosErro = erro;
    }

    public ErroRequisicaoException(ErroRequisicaoMensagens erro, Exception innerException)
        : base(erro.ObterMensagemErro(), innerException)
    {
        DadosErro = erro;
    }
}

public interface IErroRequisicao
{
    string ObterMensagemErro();
}