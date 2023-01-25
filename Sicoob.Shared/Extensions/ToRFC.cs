/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;
using System.Globalization;

namespace Sicoob.Shared
{
    public static class ToRFC
    {
        public static string ToRFC3339(this DateTime dateTime)
            => dateTime.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo);
    }
}
