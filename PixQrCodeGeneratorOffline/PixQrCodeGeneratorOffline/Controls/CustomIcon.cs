using PixQrCodeGeneratorOffline.Extention;
using Xamarin.Forms;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomIcon : Label
    {
        public CustomIcon()
        {
            FontFamily = IconExtention.GetIconFontFamily();
        }

        public static readonly BindableProperty GlyphProperty =
           BindableProperty.Create(nameof(Glyph), typeof(string), typeof(CustomIcon), null, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
           {
               var b = (CustomIcon)bindable;
               b.Text = (string)newValue;
           });

        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public static readonly BindableProperty IconTypeProperty =
           BindableProperty.Create(nameof(IconType), typeof(FontAwesomeType), typeof(CustomIcon), null, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
           {
               var b = (CustomIcon)bindable;
               b.FontFamily = IconExtention.GetIconFontFamily((FontAwesomeType)newValue);
           });

        public FontAwesomeType IconType
        {
            get { return (FontAwesomeType)GetValue(IconTypeProperty); }
            set { SetValue(IconTypeProperty, value); }
        }
    }
}
