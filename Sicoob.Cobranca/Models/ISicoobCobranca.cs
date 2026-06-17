using System;
using Sicoob.Shared.Models;

namespace Sicoob.Cobranca.Models;

public interface ISicoobCobranca
{
    event Action<ConfiguracaoToken>? UpdateTokenEvent;
}