/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System.Text.RegularExpressions;

namespace CS.BCB.PIX.Validadores
{
    /// <summary>
    /// Valida identificadores de acordo com https://bacen.github.io/pix-api/index.html#/
    /// </summary>
    public static class ValidacaoIdentificadores
    {
        /// <summary>
        /// Pattern usado na validação de TxId
        /// </summary>
        public const string ID_TRANSACAO_PATTERN = @"^[a-zA-Z0-9]{26,35}$";
        public const string ID_TRANSACAO_E2E_PATTERN = @"^[a-zA-Z0-9]{32}$";

        private static readonly Regex rx_txId = new Regex(ID_TRANSACAO_PATTERN);
        private static readonly Regex rx_e2e = new Regex(ID_TRANSACAO_E2E_PATTERN);

        /// <summary>
        /// Valida id de transação
        /// </summary>
        public static bool ValidaTransactionId(string id)
            => isValid(id, rx_txId);

        /// <summary>
        /// Valida id de fim-a-fim
        /// </summary>
        public static bool ValidaIdE2E(string id)
            => isValid(id, rx_e2e);

        // Regex tem uma vulnerabilidade que pode ser explorada
        //  para um DOS usando entradas muito longas
        // Vou aplicar um limitador anterior à chamada do Match
        private static bool isValid(string valor, Regex regex)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return false;
            }
            if (valor.Length > 100) return false;

            return regex.IsMatch(valor);
        }
    }
}
