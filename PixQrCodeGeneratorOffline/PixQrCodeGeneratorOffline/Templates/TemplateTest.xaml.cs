using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemplateTest : StackLayout
    {
        public TemplateTest()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty CardTitleProperty =
            BindableProperty.Create(nameof(CardTitle), 
                typeof(string), typeof(TemplateTest), 
                string.Empty, 
                BindingMode.Default, 
                null, 
                propertyChanged: CardTitlePropertyChanged);

        public static readonly BindableProperty CardDescriptionProperty =
            BindableProperty.Create(nameof(CardDescription), 
                typeof(string), 
                typeof(TemplateTest), 
                string.Empty,
                BindingMode.Default,
                null,
                propertyChanged: CardDescriptionPropertyChanged);

        public string CardTitle
        {
            get => (string)GetValue(CardTitleProperty);
            set => SetValue(CardTitleProperty, value);
        }

        public string CardDescription
        {
            get => (string)GetValue(CardDescriptionProperty);
            set => SetValue(CardDescriptionProperty, value);
        }

        static void CardTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = (TemplateTest)bindable;
            b.xTextA.Text = (string)newValue;
        }

        static void CardDescriptionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = (TemplateTest)bindable;
            b.xTextB.Text = (string)newValue;
        }
    }
}