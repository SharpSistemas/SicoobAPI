/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using Newtonsoft.Json;
using Sicoob.Shared.Models.Geral;
using System;

namespace Sicoob.PIX.Models.Cobranca
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CriarCobrancaRequest
    {
        public CalendarioRequest calendario { get; set; }
        public NomeCpfCnpj devedor { get; set; }
        public LocRequest loc { get; set; }
        public Valor valor { get; set; }
        public string chave { get; set; }

        public string? solicitacaoPagador { get; set; }

        public NomeValor[] infoAdicionais { get; set; }

        public static CriarCobrancaRequest Padrao(string chave, decimal valor, int expiracaoSegundos = 3600, string? solicitacaoPagador = null)
        {
            return new CriarCobrancaRequest()
            {
                chave = chave,
                valor = new Valor()
                {
                    original = valor,
                    modalidadeAlteracao = 0 // não pode mudar
                },
                calendario = new CalendarioRequest()
                {
                    expiracao = expiracaoSegundos,
                },
                solicitacaoPagador = solicitacaoPagador,
            };
        }
    }
}
