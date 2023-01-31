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
        /// <summary>
        /// Pattern usado na validação do EndToEndIdentification que transita na PACS002, PACS004 e PACS008
        /// </summary>
        public const string ID_TRANSACAO_E2E_PATTERN = @"^[a-zA-Z0-9]{32}$";
        /// <summary>
        /// Pattern usado na validação do id gerado pelo cliente para representar unicamente uma devolução
        /// </summary>
        public const string ID_TRANSACAO_DEVOLUCAO = @"^[a-zA-Z0-9]{1,35}$";
        /// <summary>
        /// Pattern usado na validação do ReturnIdentification que transita na PACS004
        /// </summary>
        public const string ID_RETURN_IDENTIFICATION = @"^[a-zA-Z0-9]{32}$";

        private static readonly Regex rx_txId = new Regex(ID_TRANSACAO_PATTERN);
        private static readonly Regex rx_e2e = new Regex(ID_TRANSACAO_E2E_PATTERN);
        private static readonly Regex rx_txDev = new Regex(ID_TRANSACAO_DEVOLUCAO);
        private static readonly Regex rx_rtrId = new Regex(ID_RETURN_IDENTIFICATION);

        /// <summary>
        /// Valida id de transação
        /// </summary>
        public static bool ValidaTransactionId(string id)
            => isValid(id, rx_txId);

        /// <summary>
        /// Valida id de transação de devolução
        /// </summary>
        public static bool ValidaIdDevolucao(string id)
            => isValid(id, rx_txDev);

        /// <summary>
        /// Valida id de fim-a-fim
        /// </summary>
        public static bool ValidaIdE2E(string id)
            => isValid(id, rx_e2e);

        /// <summary>
        /// Valida rtrId (Return Identification) que transita na PACS004
        /// </summary>
        public static bool ValidaIdRetorno(string id)
            => isValid(id, rx_rtrId);

        private static bool isValid(string valor, Regex regex)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return false;
            }

            // Regex tem uma vulnerabilidade que pode ser explorada
            //  para um DOS usando entradas muito longas
            // Vou aplicar um limitador anterior à chamada do Match
            if (valor.Length > 100) return false;

            return regex.IsMatch(valor);
        }
    }
}
