using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemplateTitlePanel : StackLayout
    {
        public TemplateTitlePanel()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TitleProperty =
           BindableProperty.Create(nameof(Title),
               typeof(string), 
               typeof(TemplateTitlePanel),
               string.Empty,
               BindingMode.Default,
               null,
               propertyChanged: TitlePropertyChanged);

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon),
                typeof(string),
                typeof(TemplateTitlePanel),
                string.Empty,
                BindingMode.Default,
                null,
                propertyChanged: IconPropertyChanged);

        public static readonly BindableProperty TitleColorProperty =
            BindableProperty.Create(nameof(TitleColor),
                typeof(Color),
                typeof(TemplateTitlePanel),
                App.ThemeColors.TextSecondary,
                BindingMode.Default,
                null,
                propertyChanged: TitleColorPropertyChanged);

        public static readonly BindableProperty IconColorProperty =
            BindableProperty.Create(nameof(IconColor),
                typeof(Color),
                typeof(TemplateTitlePanel),
                App.ThemeColors.TextSecondary,
                BindingMode.Default,
                null,
                propertyChanged: IconColorPropertyChanged);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public Color TitleColor
        {
            get => (Color)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public Color IconColor
        {
            get => (Color)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = (TemplateTitlePanel)bindable;
            b.xTitle.Text = (string)newValue;
        }

        static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = (TemplateTitlePanel)bindable;
            b.xIcon.Glyph = (string)newValue;
        }

        static void TitleColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = (TemplateTitlePanel)bindable;
            b.xTitle.TextColor = (Color)newValue;
        }

        static void IconColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = (TemplateTitlePanel)bindable;
            b.xIcon.TextColor = (Color)newValue;
        }
    }
}