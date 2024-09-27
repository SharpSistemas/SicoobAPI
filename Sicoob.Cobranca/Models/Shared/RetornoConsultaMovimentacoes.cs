namespace Sicoob.Cobranca.Models.Shared;

public class RetornoConsultaMovimentacoes
{
    public string quantidadeTotalRegistros { get; set; }
    public int quantidadeRegistrosArquivo { get; set; }
    public int quantidadeArquivo { get; set; }
    public int[] idArquivos { get; set; }
}