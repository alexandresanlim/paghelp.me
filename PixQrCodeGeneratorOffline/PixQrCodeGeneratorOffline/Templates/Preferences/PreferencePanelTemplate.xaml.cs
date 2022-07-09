using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Templates.Preferences
{
    public partial class PreferencePanelTemplate : StackLayout
    {
        public PreferencePanelTemplate()
        {
            InitializeComponent();
        }


        public static readonly BindableProperty TitleProperty =
           BindableProperty.Create(
               propertyName: nameof(Title),
               returnType: typeof(string),
               declaringType: typeof(PreferencePanelTemplate),
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
            if(bindable is PreferencePanelTemplate control && newValue != null && newValue is string value)
            {
                control.xTitle.Text = value;
            }            
        }


        public static readonly BindableProperty DescriptionProperty =
           BindableProperty.Create(
               propertyName: nameof(Description),
               returnType: typeof(string),
               declaringType: typeof(PreferencePanelTemplate),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: DescriptionPropertyChanged);

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        static void DescriptionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is PreferencePanelTemplate control && newValue != null && newValue is string value)
            {
                control.xDescription.Text = value;
            }
        }

        public static readonly BindableProperty IconProperty =
           BindableProperty.Create(
               propertyName: nameof(Icon),
               returnType: typeof(string),
               declaringType: typeof(PreferencePanelTemplate),
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
            if (bindable is PreferencePanelTemplate control && newValue != null && newValue is string value)
            {
                control.xIcon.Glyph = value;
            }            
        }

        public static readonly BindableProperty IsBetaProperty =
           BindableProperty.Create(
               propertyName: nameof(IsBeta),
               returnType: typeof(bool),
               declaringType: typeof(PreferencePanelTemplate),
               defaultValue: false,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: IsBetaPropertyChanged);

        public bool IsBeta
        {
            get => (bool)GetValue(IsBetaProperty);
            set => SetValue(IsBetaProperty, value);
        }

        static void IsBetaPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is PreferencePanelTemplate control && newValue != null && newValue is bool value)
            {
                control.xBetaInfo.IsVisible = value;
            }
        }
    }
}