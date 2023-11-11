using PixQrCodeGeneratorOffline.Services;
using Xamarin.Forms;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Templates.Key
{
    public partial class HorizontalKeys : Grid
    {
        const string BULLET = "● ● ● ● ● ● ● ● ●";

        //private static string CurrentKeyValue;

        private static bool CurrentKeyIsHide;

        public HorizontalKeys()
        {
            InitializeComponent();

            CurrentKeyIsHide = Preference.HideData;
        }

        public static readonly BindableProperty QrCodeProperty =
           BindableProperty.Create(
               propertyName: nameof(QrCode),
               returnType: typeof(string),
               declaringType: typeof(HorizontalKeys),
               propertyChanged: QrCodePropertyChanged);

        public string QrCode
        {
            get => (string)GetValue(QrCodeProperty);
            set => SetValue(QrCodeProperty, value);
        }

        static void QrCodePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is string value && !string.IsNullOrEmpty(value))
                template.xQrCode.Content = value;
        }


        public static readonly BindableProperty BoxColorProperty =
           BindableProperty.Create(
               propertyName: nameof(BoxColor),
               returnType: typeof(Color),
               declaringType: typeof(HorizontalKeys),
               defaultValue: Color.White,
               propertyChanged: BoxColorPropertyChanged);

        public Color BoxColor
        {
            get => (Color)GetValue(BoxColorProperty);
            set => SetValue(BoxColorProperty, value);
        }

        static void BoxColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is Color value && value != null)
                template.xMainFrame.BackgroundColor = value;
        }

        public static readonly BindableProperty IconInstitutionProperty =
           BindableProperty.Create(
               propertyName: nameof(IconInstitution),
               returnType: typeof(string),
               declaringType: typeof(HorizontalKeys),
               propertyChanged: IconInstitutionPropertyChanged);

        public string IconInstitution
        {
            get => (string)GetValue(IconInstitutionProperty);
            set => SetValue(IconInstitutionProperty, value);
        }

        static void IconInstitutionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is HorizontalKeys template && newValue is string value && !string.IsNullOrEmpty(value))
                template.xIconInstitution.Glyph = value;
        }

        public static readonly BindableProperty IconTypeProperty =
           BindableProperty.Create(
               propertyName: nameof(IconType),
               returnType: typeof(FontAwesomeType),
               declaringType: typeof(HorizontalKeys),
               propertyChanged: IconTypePropertyChanged);

        public FontAwesomeType IconType
        {
            get => (FontAwesomeType)GetValue(IconInstitutionProperty);
            set => SetValue(IconInstitutionProperty, value);
        }

        static void IconTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is FontAwesomeType value)
                template.xIconInstitution.IconType = value;
        }


        


        //public static readonly BindableProperty IconKeyProperty =
        //   BindableProperty.Create(
        //       propertyName: nameof(IconKey),
        //       returnType: typeof(string),
        //       declaringType: typeof(HorizontalKeys),
        //       defaultValue: string.Empty,
        //       defaultBindingMode: BindingMode.Default,
        //       validateValue: null,
        //       propertyChanged: IconKeyPropertyChanged);

        //public string IconKey
        //{
        //    get => (string)GetValue(IconKeyProperty);
        //    set => SetValue(IconKeyProperty, value);
        //}

        //static void IconKeyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    if (newValue == null)
        //        return;

        //    var template = (HorizontalKeys)bindable;
        //    string value = (string)newValue;

        //    template.xIconKey.Glyph = value;
        //}


        public static readonly BindableProperty InstitutionValueProperty =
           BindableProperty.Create(
               propertyName: nameof(InstitutionValue),
               returnType: typeof(string),
               declaringType: typeof(HorizontalKeys),
               propertyChanged: IntitutionValuePropertyChanged);

        public string InstitutionValue
        {
            get => (string)GetValue(InstitutionValueProperty);
            set => SetValue(InstitutionValueProperty, value);
        }

        static void IntitutionValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is string value && !string.IsNullOrWhiteSpace(value))
                template.xInstitutionValue.Text = value;
        }


        public static readonly BindableProperty KeyValueProperty =
           BindableProperty.Create(
               propertyName: nameof(KeyValue),
               returnType: typeof(string),
               declaringType: typeof(HorizontalKeys),
               propertyChanged: KeyValuePropertyChanged);

        public string KeyValue
        {
            get => (string)GetValue(KeyValueProperty);
            set => SetValue(KeyValueProperty, value);
        }

        static void KeyValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is string value && !string.IsNullOrEmpty(value))
            { 
                if (value != BULLET)
                    template.xKeyValueHide.Text = value;

                template.xKeyValue.Text = CurrentKeyIsHide ? value : BULLET;
            }
        }

        public static readonly BindableProperty HideValueProperty =
           BindableProperty.Create(
               propertyName: nameof(HideValue),
               returnType: typeof(bool),
               declaringType: typeof(HorizontalKeys),
               defaultValue: Preference.HideData,
               propertyChanged: HideValuePropertyChanged);

        public bool HideValue
        {
            get => (bool)GetValue(HideValueProperty);
            set => SetValue(HideValueProperty, value);
        }

        private static void HideValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is bool value)
                template.xKeyValue.Text = value ? template.xKeyValueHide.Text : BULLET;
        }
    }
}