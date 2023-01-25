/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.PIX.Models.Cobranca;

using Sicoob.Shared.Models.Geral;
using System;

public class ConsultaRequest
{
    public DateTime inicio { get; set; }
    public DateTime fim { get; set; }
    public string? cpf { get; set; }
    public string? cnpj { get; set; }
    public bool? locationPresente { get; set; }
    public string? status { get; set; }

    public RequestPaginacao? paginacao { get; set; }
}
public class ConsultaResponse
{
    public ResponseParametros parametros { get; set; }
    public CobrancaCompleta[] cobs { get; set; }
}
