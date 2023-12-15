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


public class MovimentacoesArquivo
{
    public string siglaMovimento { get; set; }
    public DateTime dataInicioMovimento { get; set; }
    public DateTime dataFimMovimento { get; set; }
    public int numeroCliente { get; set; }
    public int numeroContrato { get; set; }
    public int modalidade { get; set; }
    public int numeroTitulo { get; set; }
    public string seuNumero { get; set; }
    public DateTime dataVencimentoTitulo { get; set; }
    public decimal valorTitulo { get; set; }
    public string codigoBarras { get; set; }
    public int numeroContaCorrente { get; set; }
    public decimal valorTarifaMovimento { get; set; }
    public decimal valorAbatimento { get; set; }
    public DateTime dataMovimentoLiquidacao { get; set; }
    public DateTime dataLiquidacao { get; set; }
    public DateTime dataPrevisaoCredito { get; set; }
    public int numeroBancoRecebedor { get; set; }
    public int numeroAgenciaRecebedora { get; set; }
    public int idTipoOpFinanceira { get; set; }
    public string tipoOpFinanceira { get; set; }
    public decimal valorDesconto { get; set; }
    public decimal valorMora { get; set; }
    public decimal valorLiquido { get; set; }
}
