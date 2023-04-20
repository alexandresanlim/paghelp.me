
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Templates.Key
{
    public partial class HorizontalKeys : StackLayout
    {
        public HorizontalKeys()
        {
            InitializeComponent();
            xHidePanel.IsVisible = Preference.HideData;
        }

        public static readonly BindableProperty QrCodeProperty =
           BindableProperty.Create(
               propertyName: nameof(QrCode),
               returnType: typeof(string),
               declaringType: typeof(HorizontalKeys),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: QrCodePropertyChanged);

        public string QrCode
        {
            get => (string)GetValue(QrCodeProperty);
            set => SetValue(QrCodeProperty, value);
        }

        static void QrCodePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalKeys)bindable;
            string value = (string)newValue;

            template.xQrCode.Content = value;
        }


        public static readonly BindableProperty BoxColorProperty =
           BindableProperty.Create(
               propertyName: nameof(BoxColor),
               returnType: typeof(Color),
               declaringType: typeof(HorizontalKeys),
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

            var template = (HorizontalKeys)bindable;
            Color value = (Color)newValue;

            template.xBoxView.BackgroundColor = value;
        }

        public static readonly BindableProperty IconInstitutionProperty =
           BindableProperty.Create(
               propertyName: nameof(IconInstitution),
               returnType: typeof(string),
               declaringType: typeof(HorizontalKeys),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: IconInstitutionPropertyChanged);

        public string IconInstitution
        {
            get => (string)GetValue(IconInstitutionProperty);
            set => SetValue(IconInstitutionProperty, value);
        }

        static void IconInstitutionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalKeys)bindable;
            string value = (string)newValue;

            template.xIconInstitution.Glyph = value;
        }


        public static readonly BindableProperty IconKeyProperty =
           BindableProperty.Create(
               propertyName: nameof(IconKey),
               returnType: typeof(string),
               declaringType: typeof(HorizontalKeys),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: IconKeyPropertyChanged);

        public string IconKey
        {
            get => (string)GetValue(IconKeyProperty);
            set => SetValue(IconKeyProperty, value);
        }

        static void IconKeyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalKeys)bindable;
            string value = (string)newValue;

            template.xIconKey.Glyph = value;
        }


        public static readonly BindableProperty InstitutionValueProperty =
           BindableProperty.Create(
               propertyName: nameof(InstitutionValue),
               returnType: typeof(string),
               declaringType: typeof(HorizontalKeys),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: IntitutionValuePropertyChanged);

        public string InstitutionValue
        {
            get => (string)GetValue(InstitutionValueProperty);
            set => SetValue(InstitutionValueProperty, value);
        }

        static void IntitutionValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalKeys)bindable;
            string value = (string)newValue;

            template.xInstitutionValue.Text = value;
        }


        public static readonly BindableProperty KeyValueProperty =
           BindableProperty.Create(
               propertyName: nameof(KeyValue),
               returnType: typeof(string),
               declaringType: typeof(HorizontalKeys),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: KeyValuePropertyChanged);

        public string KeyValue
        {
            get => (string)GetValue(KeyValueProperty);
            set => SetValue(KeyValueProperty, value);
        }

        static void KeyValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalKeys)bindable;
            string value = (string)newValue;

            template.xKeyValue.Text = value;
        }

        //public static readonly BindableProperty TapCommandProperty =
        //    BindableProperty.Create(nameof(TapCommand),
        //        typeof(ICommand),
        //        typeof(HorizontalKeys),
        //        null,
        //        BindingMode.Default,
        //        null,
        //        propertyChanged: TapPropertyChanged);

        //public ICommand TapCommand
        //{
        //    get => (ICommand)GetValue(TapCommandProperty);
        //    set => SetValue(TapCommandProperty, value);
        //}

        //static void TapPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    if (newValue == null)
        //        return;

        //    var b = (HorizontalKeys)bindable;
        //    b.GestureRecognizers.Add(new TapGestureRecognizer
        //    {
        //        Command = (ICommand)newValue,
        //    });
        //}


        public static readonly BindableProperty HideValueProperty =
           BindableProperty.Create(
               propertyName: nameof(HideValue),
               returnType: typeof(bool),
               declaringType: typeof(HorizontalKeys),
               defaultValue: Preference.HideData,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: HideValuePropertyChanged);

        public bool HideValue
        {
            get => (bool)GetValue(HideValueProperty);
            set => SetValue(HideValueProperty, value);
        }

        private static void HideValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalKeys)bindable;
            bool value = (bool)newValue;

            template.xHidePanel.IsVisible = value;
        }

        public static readonly BindableProperty HidePanelColorProperty =
           BindableProperty.Create(
               propertyName: nameof(HidePanelColor),
               returnType: typeof(Color),
               declaringType: typeof(HorizontalKeys),
               defaultValue: Color.White,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: HidePanelColorPropertyChanged);

        public Color HidePanelColor
        {
            get => (Color)GetValue(HidePanelColorProperty);
            set => SetValue(HidePanelColorProperty, value);
        }

        private static void HidePanelColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var template = (HorizontalKeys)bindable;
            Color value = (Color)newValue;

            template.xHidePanel.BackgroundColor = value;
        }
    }
}