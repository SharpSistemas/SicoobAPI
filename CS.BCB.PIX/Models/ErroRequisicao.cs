/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace CS.BCB.PIX.Models
{
    public class ErroRequisicao
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string CorrelationId { get; set;}
        public Violacao[] Violacoes { get; set; }
    }
    public class Violacao
    {
        public string Razao { get; set; }
        public string Propriedade { get; set; }
        public string Valor { get; set; }
    }
}
