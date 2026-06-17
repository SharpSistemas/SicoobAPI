/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Conta.Models.Shared;

public class ResultadoResponse<T>
{
    public Mensagem[] Mensagens { get; set; }
    public T resultado { get; set; }
}
public class Mensagem
{
    public string codigo { get; set; }
    public string mensagem { get; set;}
}
