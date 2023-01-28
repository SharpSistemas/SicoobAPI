using Newtonsoft.Json;

namespace CS.BCB.PIX.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class NovaCobranca
    {
        public CalendarioCriacao calendario { get; set; }
        public NomeCpfCnpj devedor { get; set; }
        public DadosLocCriacao loc { get; set; }
        public Valor valor { get; set; }
        public string chave { get; set; }

        public string? solicitacaoPagador { get; set; }
        public NomeValor[] infoAdicionais { get; set; }

        public static NovaCobranca Padrao(string chave, decimal valor, int expiracaoSegundos = 3600, string? solicitacaoPagador = null)
        {
            return new NovaCobranca()
            {
                chave = chave,
                valor = new Valor()
                {
                    original = valor,
                    modalidadeAlteracao = 0 // não pode mudar
                },
                calendario = new CalendarioCriacao()
                {
                    expiracao = expiracaoSegundos,
                },
                solicitacaoPagador = solicitacaoPagador,
            };
        }
    }
}
