/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sicoob.Shared
{
    public static class ClassToKVP
    {
        public static KeyValuePair<string, string>[] ToKVP<T>(this T p) where T : class
            => toKVP(p).ToArray();
        private static IEnumerable<KeyValuePair<string, string>> toKVP(object p, string? prepend = null)
        {
            var type = p.GetType();

            foreach (var prop in type.GetProperties())
            {
                object? value = prop.GetValue(p);
                if (value == null) continue;

                string key;
                if (prepend == null) key = prop.Name;
                else key = prepend + prop.Name;

                string? sValue;

                var propType = prop.PropertyType;
                if (propType.IsGenericType && propType.GenericTypeArguments != null && propType.GenericTypeArguments.Length > 0)
                {
                    // Null já foi
                    propType = propType.GenericTypeArguments[0];
                }

                if (propType.IsPrimitive)
                {
                    if (propType == typeof(decimal))
                    {
                        sValue = ((decimal)value).ToString(CultureInfo.InvariantCulture);
                    }
                    else if (propType == typeof(float))
                    {
                        sValue = ((float)value).ToString(CultureInfo.InvariantCulture);
                    }
                    else if (propType == typeof(double))
                    {
                        sValue = ((double)value).ToString(CultureInfo.InvariantCulture);
                    }
                    else if (propType == typeof(int))
                    {
                        sValue = ((int)value).ToString();
                    }
                    else if (propType == typeof(bool))
                    {
                        sValue = ((bool)value) ? "true" : "false";
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else if (propType == typeof(string))
                {
                    sValue = value?.ToString();
                }
                else if (propType == typeof(DateTime))
                {
                    sValue = ((DateTime)value).ToRFC3339();
                }
                else if (propType.IsClass)
                {
                    var objKP = toKVP(value, key + ".");

                    foreach (var kp in objKP) yield return kp;
                    continue;
                }
                else
                {
                    throw new NotImplementedException();
                }

                if (sValue == null) continue;
                yield return new KeyValuePair<string, string>(
                    key: key,
                    value: sValue
                );
            }
        }
    }
}
