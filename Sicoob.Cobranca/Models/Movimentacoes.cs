namespace Sicoob.Cobranca.Models;

using System;

public class SolicitacaoMovimentacoesCarteira
{
    public int numeroContrato { get; set; }
    /// <summary>
    /// 1. Entrada 2. Prorrogação 3. A Vencer 4. Vencido 5. Liquidação 6. Baixa
    /// </summary>
    public int tipoMovimento { get; set; }
    public DateTime dataInicial { get; set; }
    public DateTime dataFinal { get; set; }
}

public class ResponseMovimentacao<T>
{
    public T resultado { get; set; }
}
public class RetornoSolicitacaoMovimentacoesCarteira
{
    public string mensagem { get; set; }
    public int codigoSolicitacao { get; set; }
}
public class RetornoConsultaMovimentacoes
{
    public string quantidadeTotalRegistros { get; set; }
    public int quantidadeRegistrosArquivo { get; set; }
    public int quantidadeArquivo { get; set; }
    public int[] idArquivos { get; set; }
}
public class RetornoArquivoMovimentacao
{
    public string arquivo { get; set; }
    public string nomeArquivo { get; set; }
}
