using System;

namespace Sicoob.Cobranca.Models.v3;

public class MovimentacaoRequest
{
    public int numeroCliente { get; set; }
    /// <summary>
    /// 1. Entrada 2. Prorrogação 3. A Vencer 4. Vencido 5. Liquidação 6. Baixa
    /// </summary>
    public int tipoMovimento { get; set; }
    public DateTime dataInicial { get; set; }
    public DateTime dataFinal { get; set; }   
}