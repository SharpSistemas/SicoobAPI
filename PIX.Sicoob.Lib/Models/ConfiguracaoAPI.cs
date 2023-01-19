namespace PIX.Sicoob.Lib.Models
{
    public class ConfiguracaoAPI
    {
        public string ClientId { get; set; }

        public string UrlAutenticacao { get; set; } = "https://auth.sicoob.com.br/auth/realms/cooperado/protocol/openid-connect/";
        public string UrlCertificadoPFX { get; set; }
        public string CertificadoSenha { get; set; }


        public string UrlApi { get; set; } = "https://api.sicoob.com.br/";

    }
}
