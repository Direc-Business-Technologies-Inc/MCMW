using System;
using System.Data;
using System.Linq;

namespace DirecLayer
{
    public class Validation
    {
        #region Validation
        public static bool isNull(params string[] values)
        {
            bool output = false;
            var value = values.Where(x => string.IsNullOrEmpty(x));
            output = value.Any();
            return output;
        }
        #endregion

        public static string UrlValid(string url)
        {
            string output = "";

            output = $"{url}";
            const string httpStr = "http://";
            const string httpsStr = "https://";
            if (!output.StartsWith(httpStr, true, null) &&
                !output.StartsWith(httpsStr, true, null))
            {
                output = $"{httpStr}{output}";
            }

            return output;
        }
    }
}
