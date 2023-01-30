/**************************************\
 * Biblioteca C# para APIs do PIX     *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System.Text.RegularExpressions;

namespace CS.BCB.PIX.Validadores
{
    /// <summary>
    /// Valida chaves de acordo com https://www.bcb.gov.br/content/estabilidadefinanceira/pix/API-DICT.html#tag/Key
    /// </summary>
    public static class ValidacaoChaves
    {
        /// <summary>
        /// Pattern usado na validação de CPF
        /// </summary>
        public const string CHAVE_CPF_PATTERN = @"^[0-9]{11}$";
        /// <summary>
        /// Pattern usado na validação de CNPJ
        /// </summary>
        public const string CHAVE_CNPJ_PATTERN = @"^[0-9]{14}$";
        /// <summary>
        /// Pattern usado na validação de Celular
        /// </summary>
        public const string CHAVE_PHONE_PATTERN = @"^\+[1-9][0-9]\d{1,14}$";
        /// <summary>
        /// Pattern usado na validação de e-mail
        /// No manual, o pattern está bugado na grade, usarei o pattern no HTML5
        /// </summary>
        public const string CHAVE_EMAIL_PATTERN = @"/^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/";
        /// <summary>
        /// Pattern usado na validação de chaves EVP (Endereço Virtual de Pagamento)
        /// </summary>
        public const string CHAVE_EVP_PATTERN = @"[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}";

        private static readonly Regex rx_cpf = new Regex(CHAVE_CPF_PATTERN);
        private static readonly Regex rx_cnpj = new Regex(CHAVE_CNPJ_PATTERN);
        private static readonly Regex rx_phone = new Regex(CHAVE_PHONE_PATTERN);
        private static readonly Regex rx_email = new Regex(CHAVE_EMAIL_PATTERN);
        private static readonly Regex rx_evp = new Regex(CHAVE_EVP_PATTERN);

        /// <summary>
        /// Valida chaves de CPF
        /// </summary>
        public static bool ValidaChave_CPF(string chave)
            => isValid(chave, rx_cpf);
        /// <summary>
        /// Valida chaves de CNPJ
        /// </summary>
        public static bool ValidaChave_CNPJ(string chave)
            => isValid(chave, rx_cnpj);
        /// <summary>
        /// Valida chave de celular
        /// </summary>
        public static bool ValidaChave_PHONE(string chave)
            => isValid(chave, rx_phone);
        /// <summary>
        /// Valida chave de e-mail
        /// </summary>
        public static bool ValidaChave_EMAIL(string chave)
            => isValid(chave, rx_email);
        /// <summary>
        /// Valida chave aleatória (EVP: Endereço Virtual de Pagamento)
        /// </summary>
        public static bool ValidaChave_EVP(string chave)
            => isValid(chave, rx_evp);

        // Regex tem uma vulnerabilidade que pode ser explorada
        //  para um DOS usando entradas muito longas
        // Como as chaves já tem um limite de 77 caracteres,
        //  vou usar o mesmo limitador
        private static bool isValid(string chave, Regex regex)
        {
            if (string.IsNullOrEmpty(chave))
            {
                return false;
            }
            if (chave.Length > 100) return false;

            return regex.IsMatch(chave);
        }


    }
}
