using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Templates
{
    public partial class TemplateAction : Frame
    {
        public TemplateAction()
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

        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(nameof(TapCommand),
                typeof(ICommand),
                typeof(TemplateTitlePanel),
                null,
                BindingMode.Default,
                null,
                propertyChanged: TapPropertyChanged);

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

        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = (TemplateAction)bindable;
            b.xTitle.Text = (string)newValue;
        }

        static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = (TemplateAction)bindable;
            b.xIcon.Glyph = (string)newValue;
        }

        static void TapPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = (TemplateAction)bindable;
            b.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = (ICommand)newValue,
            });
        }

        public static readonly BindableProperty IsNewProperty =
           BindableProperty.Create(propertyName: nameof(IsNew),
                               returnType: typeof(bool),
                               declaringType: typeof(TemplateAction),
                               defaultValue: false,
                               defaultBindingMode: BindingMode.Default,
                               propertyChanged: IsNewPropertyChanged);
        public bool IsNew
        {
            get => (bool)GetValue(IsNewProperty);
            set => SetValue(IsNewProperty, value);
        }

        static void IsNewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TemplateAction control && newValue is bool value)
            {
                control.tagNew.IsVisible = value;
            }
        } 
    }
}