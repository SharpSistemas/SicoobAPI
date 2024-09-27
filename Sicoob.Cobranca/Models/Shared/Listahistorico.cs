using System;

namespace Sicoob.Cobranca.Models.Shared;

public class Listahistorico
{
    public DateTime? dataHistorico { get; set; }
    public int? tipoHistorico { get; set; }
    public string? descricaoHistorico { get; set; }
}