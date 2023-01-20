using System.Collections.Generic;

namespace Sicoob.PIX.Models
{
    public class ConfiguracaoPIX : Shared.Models.Configuracao
    {
        public string UrlApi { get; set; } = "https://api.sicoob.com.br/";

    }
}
