/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
namespace Sicoob.Shared;

using System;
using System.Globalization;

public static class ToRFC
{
    public const string RFC_3339_FORMAT = "yyyy-MM-dd'T'HH:mm:ss.fffzzz";

    public static string ToRFC3339(this DateTime dateTime)
        => dateTime.ToUniversalTime().ToString(RFC_3339_FORMAT, DateTimeFormatInfo.InvariantInfo);
}
