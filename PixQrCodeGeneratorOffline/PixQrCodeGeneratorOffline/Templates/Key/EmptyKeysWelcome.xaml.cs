using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Templates.Key
{
    public partial class EmptyKeysWelcome : StackLayout
    {
        public EmptyKeysWelcome()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty NavigateToAddKeyCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(NavigateToAddKeyCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(EmptyKeysWelcome),
                propertyChanged: TapPropertyChanged);

        public ICommand NavigateToAddKeyCommand
        {
            get => (ICommand)GetValue(NavigateToAddKeyCommandProperty);
            set => SetValue(NavigateToAddKeyCommandProperty, value);
        }

        static void TapPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EmptyKeysWelcome template && newValue is ICommand value)
                template.xActionAddNewKey.TapCommand = value;
        }
    }
}