using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4People.StatisticalTestTask
{
    public static class CustomMath
    {
        public static double DoubleParseAdvanced(this string strToParse, char decimalSymbol = ',')
        {
            string tmp = Regex.Match(strToParse, @"([-]?[0-9]+)([\s])?([0-9]+)?[." + decimalSymbol + "]?([0-9 ]+)?([0-9]+)?").Value;

            if (tmp.Length > 0 && strToParse.Contains(tmp))
            {
                var currDecSeparator = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                tmp = tmp.Replace(".", currDecSeparator).Replace(decimalSymbol.ToString(), currDecSeparator);

                return double.Parse(tmp);
            }

            return 0;
        }

        public static string ToStringSafe(this object @object)
        {
            return (@object ?? string.Empty).ToString();

        }
    }
}
