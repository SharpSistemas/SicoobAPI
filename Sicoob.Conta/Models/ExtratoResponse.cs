/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Conta.Models;

using System;

public class ExtratoResponse
{
    public decimal Saldo { get; set; }
    public Transacao[] Transacoes { get; set; }

}
public class Transacao
{
    public string Tipo { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public DateTime DataLote { get; set; }
    public string Descricao { get; set; }
    public string NumeroDocumento { get; set; }
    public string CpfCnpj { get; set; }
    public string DescInfComplementar { get; set; }

    public override string ToString() => $"{Data:d} {Valor:C2} {Descricao}";
}
