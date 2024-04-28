using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Services;
using System.Windows.Input;
using Xamarin.Forms;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Templates.Key
{
    public partial class HorizontalKeys : Grid
    {
        const string BULLET = "● ● ● ● ● ● ● ● ●";

        private static bool CurrentKeyIsHide;

        public HorizontalKeys()
        {
            InitializeComponent();

            CurrentKeyIsHide = Preference.HideData;
        }

        public static readonly BindableProperty BoxColorProperty =
           BindableProperty.Create(
               propertyName: nameof(BoxColor),
               returnType: typeof(Color),
               declaringType: typeof(HorizontalKeys),
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
            if (bindable is HorizontalKeys template && newValue is Color value && value != null)
                template.xInstitutionValue.TextColor =
                    template.xIconInstitutionTitle.TextColor =
                    template.xKeyValue.TextColor =
                    template.xKeyValueHide.TextColor =
                    template.xFourthIconContent.TextColor =
                    template.xPrimaryIconContent.TextColor =
                    template.xSecondaryIconContent.TextColor =
                    template.xThirdIconContent.TextColor =
                    template.xIconInstitution.TextColor = value;
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
                template.xIconInstitution.Glyph = template.xIconInstitutionTitle.Glyph = value;

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
                template.xIconInstitution.IconType = template.xIconInstitutionTitle.IconType = value;
        }

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


        public static readonly BindableProperty PrimaryIconCommandProperty =
          BindableProperty.Create(
              propertyName: nameof(PrimaryIconCommand),
              returnType: typeof(ICommand),
              declaringType: typeof(HorizontalKeys),
              propertyChanged: PrimaryIconCommandChanged);

        public ICommand PrimaryIconCommand
        {
            get => (ICommand)GetValue(PrimaryIconCommandProperty);
            set => SetValue(PrimaryIconCommandProperty, value);
        }

        static void PrimaryIconCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is ICommand value && value != null)
            {
                template.xPrimaryIcon.GestureRecognizers.Clear();
                template.xPrimaryIcon.GestureRecognizers.Add(new TapGestureRecognizer { Command = value });
            }
        }

        public static readonly BindableProperty SecondaryIconCommandProperty =
          BindableProperty.Create(
              propertyName: nameof(SecondaryIconCommand),
              returnType: typeof(ICommand),
              declaringType: typeof(HorizontalKeys),
              propertyChanged: SecondaryIconCommandChanged);

        public ICommand SecondaryIconCommand
        {
            get => (ICommand)GetValue(SecondaryIconCommandProperty);
            set => SetValue(SecondaryIconCommandProperty, value);
        }

        static void SecondaryIconCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is ICommand value && value != null)
            {
                template.xSecondaryIcon.GestureRecognizers.Clear();
                template.xSecondaryIcon.GestureRecognizers.Add(new TapGestureRecognizer { Command = value });
            }
        }

        public static readonly BindableProperty ThirdIconCommandProperty =
          BindableProperty.Create(
              propertyName: nameof(ThirdIconCommand),
              returnType: typeof(ICommand),
              declaringType: typeof(HorizontalKeys),
              propertyChanged: ThirdIconCommandChanged);

        public ICommand ThirdIconCommand
        {
            get => (ICommand)GetValue(ThirdIconCommandProperty);
            set => SetValue(ThirdIconCommandProperty, value);
        }

        static void ThirdIconCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is ICommand value && value != null)
            {
                template.xThirdIcon.GestureRecognizers.Clear();
                template.xThirdIcon.GestureRecognizers.Add(new TapGestureRecognizer { Command = value });
            }   
        }

        public static readonly BindableProperty FourthIconCommandProperty =
         BindableProperty.Create(
             propertyName: nameof(FourthIconCommand),
             returnType: typeof(ICommand),
             declaringType: typeof(HorizontalKeys),
             propertyChanged: FourthIconCommandChanged);

        public ICommand FourthIconCommand
        {
            get => (ICommand)GetValue(FourthIconCommandProperty);
            set => SetValue(FourthIconCommandProperty, value);
        }

        static void FourthIconCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is HorizontalKeys template && newValue is ICommand value && value != null)
            {
                template.xFourthIcon.GestureRecognizers.Clear();
                template.xFourthIcon.GestureRecognizers.Add(new TapGestureRecognizer { Command = value });
            }
        }
    }
}