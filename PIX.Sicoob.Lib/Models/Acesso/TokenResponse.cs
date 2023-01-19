namespace PIX.Sicoob.Lib.Models.Acesso
{
    public class TokenResponse
    { 
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string token_type { get; set; }
        public int notbeforepolicy { get; set; }
        public string scope { get; set; }
    }

}
