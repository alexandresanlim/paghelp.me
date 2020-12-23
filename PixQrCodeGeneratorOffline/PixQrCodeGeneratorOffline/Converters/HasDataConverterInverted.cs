using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;


namespace PixQrCodeGeneratorOffline.Converters
{
    public class HasDataConverterInverted : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)new HasDataConverter().Convert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
