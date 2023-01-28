namespace CS.BCB.PIX.Models
{
    public class NovaCobranca
    {
        public CalendarioCriacao calendario { get; set; }
        public NomeCpfCnpj devedor { get; set; }
        public DadosLocCriacao loc { get; set; }
        public Valor valor { get; set; }
        public string chave { get; set; }

        public string? solicitacaoPagador { get; set; }

        public NomeValor[] infoAdicionais { get; set; }
    }
}
