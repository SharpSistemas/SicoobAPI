﻿using System.Collections.Generic;

namespace Sicoob.PIX.Lib.Models
{
    public class ConfiguracaoPIX : Shared.Models.Configuracao
    {
        public string UrlApi { get; set; } = "https://api.sicoob.com.br/";

    }
}