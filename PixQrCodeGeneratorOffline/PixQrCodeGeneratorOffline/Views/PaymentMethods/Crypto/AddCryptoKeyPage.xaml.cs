using PixQrCodeGeneratorOffline.ViewModels.PaymentMethods.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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