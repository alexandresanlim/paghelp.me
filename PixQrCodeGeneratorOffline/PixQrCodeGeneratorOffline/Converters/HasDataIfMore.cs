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
            if (value == null)
                return value;

            var v = (IList)value;

            if (parameter != null)
            {
                if (parameter is int parameterInt)
                    return v.Count > parameterInt;

                if (parameter is string parameterString && int.TryParse(parameterString, out int stringConvert))
                    return v.Count > stringConvert;
            }

            return v.Count > 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }


}
