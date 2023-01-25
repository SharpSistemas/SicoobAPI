/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.PIX.Models.Pix;

using Sicoob.Shared.Models.Geral;
using System;

public class ConsultaRequest
{
    public DateTime inicio { get; set; }
    public DateTime fim { get; set; }
    public string? txid { get; set; }
    public bool? txIdPresente { get; set; }
    public bool? devolucaoPresente { get; set; }
    public string? cpf { get; set; }
    public string? cnpj { get; set; }

    public RequestPaginacao? paginacao { get; set; }

}
