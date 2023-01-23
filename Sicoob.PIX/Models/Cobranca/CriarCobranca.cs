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

    public class CriarCobrancaResponse
    {
        public CalendarioResponse calendario { get; set; }
        public string txid { get; set; }
        public int revisao { get; set; }
        public NomeCpfCnpj devedor { get; set; }
        public LocResponse loc { get; set; }
        public string location { get; set; }
        /// <summary>
        /// ATIVA, CONCLUIDA, REMOVIDA_PELO_USUARIO_RECEBEDOR, REMOVIDA_PELO_PSP
        /// </summary>
        public string status { get; set; }
        public Valor valor { get; set; }
        public string brcode { get; set; }
        public string chave { get; set; }
        public string solicitacaoPagador { get; set; }
        public NomeValor[] infoAdicionais { get; set; }


        public CobrancaCompleta.ListaStatus ObterStatus()
        {
            if (!Enum.TryParse(status, out CobrancaCompleta.ListaStatus result))
            {
                result = CobrancaCompleta.ListaStatus.DESCONHECIDO;
            }

            return result;
        }
    }
}
