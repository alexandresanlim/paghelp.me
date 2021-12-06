using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Templates.Preferences
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            if (newValue == null)
                return;

            var template = (PreferencePanelTemplate)bindable;
            template.xTitle.Text = (string)newValue;
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
            if (newValue == null)
                return;

            var template = (PreferencePanelTemplate)bindable;
            template.xDescription.Text = (string)newValue;
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
            if (newValue == null)
                return;

            var template = (PreferencePanelTemplate)bindable;
            template.xIcon.Glyph = (string)newValue;
        }
    }
}