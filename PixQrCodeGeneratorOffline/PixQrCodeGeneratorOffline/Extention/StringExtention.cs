using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PixQrCodeGeneratorOffline.Extention
{
    public static class StringExtention
    {
        public static decimal IsCurrencyToDecimalByCulture(this string currency)
        {
            if (string.IsNullOrEmpty(currency))
                return decimal.Zero;

            if (decimal.TryParse(currency, NumberStyles.Currency, new CultureInfo("pt-BR"), out decimal decimalReturn))
                return decimalReturn;

            else
            {
                if (decimal.TryParse(currency, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal decimalReturnByCurrrentCulture))
                    return decimalReturnByCurrrentCulture;
            }

            return decimal.Zero;
        }

        public static bool ToBoolean(this string text)
        {
            return bool.Parse(text.Replace(" ", "").Trim());
        }

        public static int ToInt(this string text)
        {
            return int.Parse(text.Replace(" ", "").Trim());
        }

        public static string ToImageFormatter(this string imageUri, string placeholder = "")
        {
            if (string.IsNullOrEmpty(imageUri))
                return placeholder;

            if (imageUri.ToLower().Contains("instagram") && !imageUri.ToLower().Contains("cdn"))
                return imageUri += "media?size=m";

            return imageUri;
        }

        public static TimeSpan ToTimeSpan(this string time)
        {
            if (TimeSpan.TryParse(time, out TimeSpan timeOutput))
                return timeOutput;

            return TimeSpan.MinValue;
        }
    }
}
