using PA_System.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PA_System.Helper
{
    public static class Extention
    {
        public static int AsInteger(this string s)
        {
            int i;

            if (Int32.TryParse(s, out i)) return i;

            return 0;
        }


        public static double AsDouble(this string s)
        {
            double value;

            if (Double.TryParse(s, out value)) return value;

            return 0;
        }

        public static float AsFloat(this string s)
        {
            float value;

            if (float.TryParse(s, out value)) return value;

            return 0;
        }

        public static string RemoveXmlNameSpace(this string xml)
        {
            return Regex.Replace(xml, @" xmlns[^""]+""[^""]+[""]+", string.Empty);
        }

        public static string StringArrayToCommaString(this List<Rater> source)
        {
            return string.Join(",", source.Select(c => c.Name).ToArray());
        }
    }
}