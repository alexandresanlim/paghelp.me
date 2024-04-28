using System.Windows.Input;
using Xamarin.Forms;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Templates.Key
{
    public partial class HorizontalActionKeys : Frame
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
               propertyChanged: TitlePropertyChanged);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is HorizontalActionKeys template && newValue is string value && !string.IsNullOrWhiteSpace(value))
                template.xTitle.Text = value;
        }

        public static readonly BindableProperty BoxColorProperty =
           BindableProperty.Create(
               propertyName: nameof(BoxColor),
               returnType: typeof(Color),
               declaringType: typeof(HorizontalActionKeys),
               defaultValue: Color.White,
               propertyChanged: BoxColorPropertyChanged);

        public Color BoxColor
        {
            get => (Color)GetValue(BoxColorProperty);
            set => SetValue(BoxColorProperty, value);
        }

        private static void BoxColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalActionKeys template && newValue is Color value)
                template.xBoxView.BackgroundColor = value;
        }

        public static readonly BindableProperty OnBoxColorProperty =
           BindableProperty.Create(
               propertyName: nameof(OnBoxColor),
               returnType: typeof(Color),
               declaringType: typeof(HorizontalKeys),
               propertyChanged: OnBoxColorPropertyChanged);

        public Color OnBoxColor
        {
            get => (Color)GetValue(OnBoxColorProperty);
            set => SetValue(OnBoxColorProperty, value);
        }

        static void OnBoxColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalActionKeys template && newValue is Color value && value != null)
                template.xIcon.TextColor = value;
        }


       

        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(TapCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(HorizontalActionKeys),
                propertyChanged: TapPropertyChanged);

        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        static void TapPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalActionKeys template && newValue is ICommand value)
                template.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = value
                });
        }

        public static readonly BindableProperty IconProperty =
           BindableProperty.Create(
               propertyName: nameof(Icon),
               returnType: typeof(string),
               declaringType: typeof(HorizontalActionKeys),
               propertyChanged: IconPropertyChanged);

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalActionKeys template && newValue is string value && !string.IsNullOrWhiteSpace(value))
                template.xIcon.Glyph = value;
        }

        public static readonly BindableProperty IconTypeProperty =
           BindableProperty.Create(
               propertyName: nameof(IconType),
               returnType: typeof(FontAwesomeType),
               declaringType: typeof(HorizontalActionKeys),
               defaultValue: FontAwesomeType.solid,
               propertyChanged: IconTypePropertyChanged);

        public FontAwesomeType IconType
        {
            get => (FontAwesomeType)GetValue(IconTypeProperty);
            set => SetValue(IconTypeProperty, value);
        }

        private static void IconTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalActionKeys template && newValue is FontAwesomeType value)
                template.xIcon.IconType = value;
        }

        public static readonly BindableProperty IconColorProperty =
          BindableProperty.Create(
              propertyName: nameof(IconColor),
              returnType: typeof(Color),
              declaringType: typeof(HorizontalActionKeys),
              defaultValue: Color.White,
              propertyChanged: IconColorPropertyChanged);

        public Color IconColor
        {
            get => (Color)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        private static void IconColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalActionKeys template && newValue is Color value)
                template.xIcon.TextColor = value;
        }

        public static readonly BindableProperty RequiresInternetProperty =
           BindableProperty.Create(
               propertyName: nameof(RequiresInternet),
               returnType: typeof(bool),
               declaringType: typeof(HorizontalActionKeys),
               defaultValue: false,
               propertyChanged: RequiresInternetPropertyChanged);

        public bool RequiresInternet
        {
            get => (bool)GetValue(RequiresInternetProperty);
            set => SetValue(RequiresInternetProperty, value);
        }

        private static void RequiresInternetPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalActionKeys control && newValue is bool value)
                control.xIconRequiresInternet.IsVisible = value;
        }
    }
}