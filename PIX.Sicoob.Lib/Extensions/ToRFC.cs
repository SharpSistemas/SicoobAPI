using System;
using System.Globalization;

namespace PIX.Sicoob.Lib
{
    public static class ToRFC
    {
        public static string ToRFC3339(this DateTime dateTime)
            => dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo);
    }
}
