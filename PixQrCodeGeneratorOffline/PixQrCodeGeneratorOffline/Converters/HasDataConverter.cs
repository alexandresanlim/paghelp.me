using System;
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

            if (value is string valueString)
                return !string.IsNullOrWhiteSpace(valueString);

            if (value is IList valueList)
                return valueList.Count > 0;

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
