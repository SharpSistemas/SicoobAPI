using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sicoob.PIX.Lib
{
    public static class ClassToKVP
    {
        public static KeyValuePair<string, string>[] ToKVP<T>(this T p) where T : class
            => toKVP(p).ToArray();
        private static IEnumerable<KeyValuePair<string, string>> toKVP(object p, string prepend = null)
        {
            var type = p.GetType();

            foreach (var prop in type.GetProperties())
            {
                var value = prop.GetValue(p);
                if (value == null) continue;

                string key;
                if (prepend == null) key = prop.Name;
                else key = prepend + prop.Name;

                string sValue;
                if (prop.PropertyType.IsPrimitive)
                {

                    if (prop.PropertyType == typeof(decimal))
                    {
                        sValue = ((decimal)value).ToString(CultureInfo.InvariantCulture);
                    }
                    else if (prop.PropertyType == typeof(float))
                    {
                        sValue = ((float)value).ToString(CultureInfo.InvariantCulture);
                    }
                    else if (prop.PropertyType == typeof(double))
                    {
                        sValue = ((double)value).ToString(CultureInfo.InvariantCulture);
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        sValue = ((int)value).ToString();
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else if (prop.PropertyType == typeof(string))
                {
                    sValue = value.ToString();
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    sValue = ((DateTime)value).ToRFC3339();
                }
                else if (prop.PropertyType.IsClass)
                {
                    var objKP = toKVP(value, key + ".");

                    foreach (var kp in objKP) yield return kp;
                    continue;
                }
                else
                {
                    throw new NotImplementedException();
                }

                yield return new KeyValuePair<string, string>(
                    key: key,
                    value: sValue
                );
            }
        }
    }
}
