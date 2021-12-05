using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Converters
{
    public class HasDataIfMore : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (IList)value;

            if (parameter == null && parameter is int)
            {
                return v.Count > (int)parameter;
            }

            return v.Count > 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }


}
