/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Shared.Models.Geral;

public class RequestPaginacao
{
    public int paginaAtual { get; set; }
    public int itensPorPagina { get; set; }
}
