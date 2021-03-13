//using System;
//using System.Globalization;
//using Xamarin.Forms;


//namespace PixQrCodeGeneratorOffline.Converters
//{
//    public class HasValueToBooleanConverter : IValueConverter
//    {
//        public object Parameter { get; set; }

//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            if (value == null || (parameter == null && Parameter == null))
//                return value;

//            if (Parameter != null)
//                parameter = Parameter;

//            var a = !value.ToString().Equals(parameter.ToString());

//            return a;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            return value;
//        }
//    }
//}
