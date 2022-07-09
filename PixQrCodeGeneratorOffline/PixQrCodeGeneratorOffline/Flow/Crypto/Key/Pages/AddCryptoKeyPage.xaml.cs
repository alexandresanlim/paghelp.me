using PixQrCodeGeneratorOffline.ViewModels.PaymentMethods.Crypto;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views.PaymentMethods.Crypto
{
    public partial class AddCryptoKeyPage : ContentPage
    {
        public AddCryptoKeyPage()
        {
            InitializeComponent();
        }

        AddCryptoKeyViewModel _viewModel;

        public AddCryptoKeyPage(Models.PaymentMethods.Crypto.CryptoKey key = null, bool isContact = false)
        {
            InitializeComponent();

            BindingContext = _viewModel = new AddCryptoKeyViewModel(key, isContact);
        }
    }
}