using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EngineX
{
    public static class ParameterValueHelper
    {
        public static bool AreEqual(object x, object y)
        {
            if (x == null && y == null) return true;
            return object.Equals(x, y) || (CanBeNumeric(x, y) && NumericEquals(x, y));
        }


        private static bool NumericEquals(object o, object o1)
        {
            if (o is string && IsStringBoolean(o as string))
            {
                o = FromStringBoolean(o as string);
            }
            if (o1 is string && IsStringBoolean(o1 as string))
            {
                o1 = FromStringBoolean(o1 as string);
            }
            var numberFormatInfo = new NumberFormatInfo()
            {
                NumberDecimalSeparator = ",",
                NumberGroupSeparator = "."
            };
            return Decimal.Compare(Convert.ToDecimal(o, numberFormatInfo), Convert.ToDecimal(o1, numberFormatInfo)) ==
                   0;
        }

        

        private static bool CanBeNumeric(params object[] o)
        {
            return o != null && o.All(z => z != null && z is bool
                                           || z is int
                                           || z is float
                                           || z is decimal
                                           || (z is string s &&
                                               (s.All(c => char.IsNumber(c) || c == '.' || c == ',') ||
                                                IsStringBoolean(s))));
        }

        private static string[] StringBooleans = new[] {"true", "false", "1", "0"};
        private static bool? FromStringBoolean(string o)
        {
            if (string.Equals(o, "true", StringComparison.OrdinalIgnoreCase) || string.Equals(o, "1"))
            {
                return true;
            }
            if (string.Equals(o, "false", StringComparison.OrdinalIgnoreCase) || string.Equals(o, "0"))
            {
                return false;
            }

            return null;
        }
        private static bool IsStringBoolean(string s)
        {
            return StringBooleans.Any(z => string.Equals(s, z, StringComparison.OrdinalIgnoreCase));
        }

        public static int GetHashCode(object o)
        {
            return 0;
        }

        public static int ToInt(object x)
        {
            return Convert.ToInt32(x);
        }
    }
}