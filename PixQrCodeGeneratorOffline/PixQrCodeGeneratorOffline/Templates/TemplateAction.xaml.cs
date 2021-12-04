using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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

        //public static readonly BindableProperty TapProperty =
        //    BindableProperty.Create(nameof(TapCommand),
        //        typeof(Command),
        //        typeof(TemplateTitlePanel),
        //        null,
        //        BindingMode.Default,
        //        null,
        //        propertyChanged: TapPropertyChanged);

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

        //public Command TapCommand
        //{
        //    get => (Command)GetValue(TapProperty);
        //    set => SetValue(TapProperty, value);
        //}

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

        //static void TapPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var b = (TemplateAction)bindable;
        //    b.xTapCommand.Command = (Command)newValue;
        //}
    }
}