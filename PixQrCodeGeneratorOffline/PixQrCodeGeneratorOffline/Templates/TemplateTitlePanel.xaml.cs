using PixQrCodeGeneratorOffline.Extention;
using System.Reflection;
using Xamarin.Forms;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Templates
{
    public partial class TemplateTitlePanel : Grid
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
               propertyChanged: TitlePropertyChanged);

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon),
                typeof(string),
                typeof(TemplateTitlePanel),
                string.Empty,
                BindingMode.Default,
                propertyChanged: IconPropertyChanged);

        public static readonly BindableProperty TitleColorProperty =
            BindableProperty.Create(nameof(TitleColor),
                typeof(Color),
                typeof(TemplateTitlePanel),
                App.ThemeColors.TextSecondary,
                BindingMode.Default,
                propertyChanged: TitleColorPropertyChanged);

        public static readonly BindableProperty IconColorProperty =
            BindableProperty.Create(nameof(IconColor),
                typeof(Color),
                typeof(TemplateTitlePanel),
                App.ThemeColors.TextSecondary,
                BindingMode.Default,
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

        public static readonly BindableProperty IconTypeProperty =
          BindableProperty.Create(
              propertyName: nameof(IconType),
              returnType: typeof(FontAwesomeType),
              declaringType: typeof(TemplateTitlePanel),
              defaultValue: FontAwesomeType.solid,
              defaultBindingMode: BindingMode.Default,
              validateValue: null,
              propertyChanged: IconTypePropertyChanged);

        public FontAwesomeType IconType
        {
            get => (FontAwesomeType)GetValue(IconTypeProperty);
            set => SetValue(IconTypeProperty, value);
        }

        private static void IconTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (TemplateTitlePanel)bindable;
            FontAwesomeType value = (FontAwesomeType)newValue;

            template.xIcon.IconType = value;
        }

        public static readonly BindableProperty SubTitleProperty =
           BindableProperty.Create(propertyName: nameof(SubTitle),
                               returnType: typeof(string),
                               declaringType: typeof(TemplateTitlePanel),
                               defaultValue: string.Empty,
                               defaultBindingMode: BindingMode.Default,
                               propertyChanged: SubTitlePropertyChanged);
        public string SubTitle
        {
            get => (string)GetValue(SubTitleProperty);
            set => SetValue(SubTitleProperty, value);
        }

        static void SubTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TemplateTitlePanel control && newValue is string value && !string.IsNullOrWhiteSpace(value))
            {
                control.xSubTitle.IsVisible = true;
                control.xSubTitle.Text = value;
            }
        } 

        public static readonly BindableProperty BackgroundImageProperty =
           BindableProperty.Create(propertyName: nameof(BackgroundImage),
                               returnType: typeof(string),
                               declaringType: typeof(TemplateTitlePanel),
                               propertyChanged: BackgroundImagePropertyChanged);
        public string BackgroundImage
        {
            get => (string)GetValue(BackgroundImageProperty);
            set => SetValue(BackgroundImageProperty, value);
        }

        static void BackgroundImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TemplateTitlePanel control && newValue is string value && !string.IsNullOrWhiteSpace(value))
            {
                control.xBackgroundImage.IsVisible = true;
                control.xBackgroundImage.Source = ImageSource.FromResource($"PixQrCodeGeneratorOffline.{value}", typeof(TemplateTitlePanel).GetTypeInfo().Assembly);
                //control.xIcon.TextColor = control.xTitle.TextColor = control.xSubTitle.TextColor = Color.White;
            }
        } 
    }
}