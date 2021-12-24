
using System.Windows.Input;
using Xamarin.Forms;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Templates.Key
{
    public partial class HorizontalActionKeys : StackLayout
    {
        public HorizontalActionKeys()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TitleProperty =
           BindableProperty.Create(
               propertyName: nameof(Title),
               returnType: typeof(string),
               declaringType: typeof(HorizontalActionKeys),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: TitlePropertyChanged);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalActionKeys)bindable;
            string value = (string)newValue;

            template.xTitle.Text = value;
        }

        public static readonly BindableProperty BoxColorProperty =
           BindableProperty.Create(
               propertyName: nameof(BoxColor),
               returnType: typeof(Color),
               declaringType: typeof(HorizontalActionKeys),
               defaultValue: Color.White,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: BoxColorPropertyChanged);

        public Color BoxColor
        {
            get => (Color)GetValue(BoxColorProperty);
            set => SetValue(BoxColorProperty, value);
        }

        static void BoxColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalActionKeys)bindable;
            Color value = (Color)newValue;

            template.xBoxView.BackgroundColor = value;
        }

        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(nameof(TapCommand),
                typeof(ICommand),
                typeof(HorizontalActionKeys),
                null,
                BindingMode.Default,
                null,
                propertyChanged: TapPropertyChanged);

        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        static void TapPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var b = (HorizontalActionKeys)bindable;
            b.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = (ICommand)newValue,
            });
        }

        public static readonly BindableProperty IconProperty =
           BindableProperty.Create(
               propertyName: nameof(Icon),
               returnType: typeof(string),
               declaringType: typeof(HorizontalActionKeys),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: IconPropertyChanged);

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalActionKeys)bindable;
            string value = (string)newValue;

            template.xIcon.Glyph = value;
        }

        public static readonly BindableProperty IconTypeProperty =
           BindableProperty.Create(
               propertyName: nameof(IconType),
               returnType: typeof(FontAwesomeType),
               declaringType: typeof(HorizontalActionKeys),
               defaultValue: FontAwesomeType.solid,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: IconTypePropertyChanged);

        public FontAwesomeType IconType
        {
            get => (FontAwesomeType)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        static void IconTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalActionKeys)bindable;
            FontAwesomeType value = (FontAwesomeType)newValue;

            template.xIcon.IconType = value;
        }
    }
}