/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Shared.Models.Acesso;

public class TokenResponse
{ 
    public string? access_token { get; set; }
    public string? id_token { get; set; }
    public int expires_in { get; set; }
    public int refresh_expires_in { get; set; }
    public string? token_type { get; set; }
    public int notbeforepolicy { get; set; }
    public string? scope { get; set; }
}
