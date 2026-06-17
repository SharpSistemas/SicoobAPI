using System;

namespace Sicoob.Conta.Models.Shared;

public abstract class TransacaoBase
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