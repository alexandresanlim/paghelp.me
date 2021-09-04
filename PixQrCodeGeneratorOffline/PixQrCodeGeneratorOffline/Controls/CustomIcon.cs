using PixQrCodeGeneratorOffline.Extention;
using Xamarin.Forms;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomIcon : Label
    {
        //private FontImageSource FontImageSource { get; set; } = new FontImageSource()
        //{
        //    Color = App.ThemeColors.TextOnSecondary,
        //    Size = Device.GetNamedSize(NamedSize.Default, typeof(FontImageSource)),
        //    FontFamily = IconExtention.GetIconFontFamily()
        //};

        public CustomIcon()
        {
            FontFamily = IconExtention.GetIconFontFamily();
            //Source = FontImageSource;
        }

        public static readonly BindableProperty IconColorProperty =
           BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(CustomIcon), null, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
           {
               var b = (CustomIcon)bindable;
               b.TextColor = (Color)newValue;
           });

        public Color IconColor
        {
            get { return (Color)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        //public static readonly BindableProperty IconSizeProperty =
        //   BindableProperty.Create(nameof(IconSize), typeof(NamedSize), typeof(CustomIcon), null, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
        //   {
        //       var b = (CustomIcon)bindable;
        //       b.FontImageSource.Size = Device.GetNamedSize((NamedSize)newValue, typeof(FontImageSource));
        //   });

        //public NamedSize IconSize
        //{
        //    get { return (NamedSize)GetValue(IconSizeProperty); }
        //    set { SetValue(IconSizeProperty, value); }
        //}

        //public static readonly BindableProperty SizeProperty =
        //   BindableProperty.Create(nameof(Size), typeof(double), typeof(CustomIcon), null, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
        //   {
        //       if (newValue == null)
        //           return;

        //       var b = (CustomIcon)bindable;
        //       b.FontImageSource.Size = (double)newValue;
        //   });

        //public double Size
        //{
        //    get { return (double)GetValue(SizeProperty); }
        //    set { SetValue(SizeProperty, value); }
        //}

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
