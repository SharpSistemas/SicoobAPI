/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.PIX.Models.Pix;

using Sicoob.Shared.Models.Geral;
using System;

public class ConsultaResponse
{
    public ResponseParametros parametros { get; set; }
    public PixResponse[] pix { get; set; }

}
public class PixResponse
{
    public string endToEndId { get; set; }
    public string txid { get; set; }
    public decimal valor { get; set; }
    public string chave { get; set; }
    public DateTime horario { get; set; }
    public string nomePagador { get; set; }
    public NomeCpfCnpj pagador { get; set; }
    public DevolucaoResponse[] devolucoes { get; set; }

    public override string ToString()
    {
        string dev = "";
        if (devolucoes != null && devolucoes.Length > 0)
        {
            dev = $" [Dev:{dev.Length}]";
        }

        return $"{horario:g} {valor:C2} {nomePagador}{dev}";
    }

}
