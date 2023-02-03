/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/

namespace Sicoob.Shared.Models
{
    public class Configuracao
    {
        /// <summary>
        /// Url do serviço OpenId
        /// </summary>
        public string UrlAutenticacao { get; set; } = "https://auth.sicoob.com.br/auth/realms/cooperado/protocol/openid-connect/";

        /// <summary>
        /// Id do cliente gerado pelo Sicoob no momento do cadastro do Aplicativo
        /// </summary>
        public string? ClientId { get; set; }
        /// <summary>
        /// Caminho do certificado com chave pública
        /// </summary>
        public string? UrlCertificadoPFX { get; set; }
        /// <summary>
        /// Senha do arquivo PFX, será apagada no construtor da classe `Sicoob`
        /// </summary>
        public string? CertificadoSenha { get; set; }
        /// <summary>
        /// Scopo de autorização, depende do serviço a ser utilizado
        /// </summary>
        public AuthorizationScope Scope { get; set; } = new AuthorizationScope();
    }
    public class ConfiguracaoAPI : Configuracao
    {
        /// <summary>
        /// Url do serviço de API
        /// </summary>
        public string UrlApi { get; set; } = "https://api.sicoob.com.br/";
    }
}
