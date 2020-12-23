﻿using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Converters
{
    public class HasDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            if (value is string)
                return !string.IsNullOrWhiteSpace((string)value);

            if (value is IList)
                return ((IList)value).Count > 0;

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
