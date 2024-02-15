using System.Globalization;
using UnityEngine;


namespace BloodMoon.Utils
{
    public static class StringUtils
    {
        public static string DisplayFloatDecimal(this float fl, int digits = 1)
        {
            float exp = Mathf.Pow(10, digits);
            int baseInteger = (int) fl;
            int leftover = (int)((fl % 1.00) * exp);
            return $"{baseInteger}.{leftover.ToString(CultureInfo.InvariantCulture).PadRight(digits, '0')}";
        }
    }
}