//using Xamarin.Forms;

//namespace PixQrCodeGeneratorOffline.Controls
//{
//    public class CustomLabel : Label
//    {
//        public CustomLabel()
//        {
//            TextColor = App.ThemeColors.TextPrimary;
//            FontFamily = "FontPoppinsRegular";
//            this.PropertyChanged += CustomLabel_PropertyChanged;
//        }

//        private void CustomLabel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
//        {
//            if(e.PropertyName.Equals(nameof(Label.FontAttributes)))
//            {
//                var s = (Label)sender;

//                switch (s.FontAttributes)
//                {
//                    default:
//                    case FontAttributes.None:
//                        break;
//                    case FontAttributes.Bold:
//                        s.FontFamily = "FontPoppinsSemiBold";
//                        break;
//                    case FontAttributes.Italic:
//                        s.FontFamily = "FontPoppinsItalic";
//                        break;
//                }
//            }
//        }

//        public static readonly BindableProperty CustomFontSizeProperty =
//           BindableProperty.Create(nameof(CustomFontSize), typeof(NamedSize), typeof(CustomLabel), NamedSize.Default, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
//           {
//               var control = (CustomLabel)bindable;
//               control.FontSize = Device.GetNamedSize((NamedSize)newValue, typeof(Label));
//           });

//        public NamedSize CustomFontSize
//        {
//            get { return (NamedSize)GetValue(CustomFontSizeProperty); }
//            set { SetValue(CustomFontSizeProperty, value); }
//        }

//        public static readonly BindableProperty CustomTextColorProperty =
//           BindableProperty.Create(nameof(CustomTextColor), typeof(Color), typeof(CustomLabel), null, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
//           {
//               var control = (CustomLabel)bindable;
//               control.TextColor = (Color)newValue;
//           });

//        public Color CustomTextColor
//        {
//            get { return (Color)GetValue(CustomTextColorProperty); }
//            set { SetValue(CustomTextColorProperty, value); }
//        }
//    }
//}
