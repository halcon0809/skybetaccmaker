using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkybetAccBot
{
    public class Utils
    {
        private static NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint;
        private static CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

        public static int GetRandValue(int maxValue)
        {
            Random random = new Random();
            return random.Next(0, maxValue);
        }

        public static string ConvertLongTime()
        {
            DateTime currentTime = DateTime.Now;
            long timestamp = (long)(currentTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return timestamp.ToString();
        }
        public static int _pon()
        {
            return GetRandValue(10) >= 5 ? 1 : -1;
        }
        public static int GetRandValue(int minValue, int maxValue, bool pon = false)
        {
            int c = maxValue - minValue + 1;
            Random random = new Random();
            return (int)Math.Floor(random.NextDouble() * c + minValue) * (pon ? _pon() : 1);
        }
        public static decimal ParseToDecimal(string str)
        {
            decimal value = 0;
            decimal.TryParse(str, style, culture, out value);
            return value;
        }
    }
}
