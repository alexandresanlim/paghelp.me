﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;

            if (value is string @string && string.IsNullOrEmpty(@string))
                return value;

            NumberFormatInfo nfi = culture.NumberFormat;
            return System.Convert.ToDecimal(value, new System.Globalization.CultureInfo("en-US")).ToString("C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueFromString = Regex.Replace(value.ToString(), @"\D", "");

            if (valueFromString.Length <= 0)
                return 0m;

            long valueLong;
            if (!long.TryParse(valueFromString, out valueLong))
                return 0m;

            if (valueLong <= 0)
                return 0m;

            return valueLong / 100m;
        }
    }
}
