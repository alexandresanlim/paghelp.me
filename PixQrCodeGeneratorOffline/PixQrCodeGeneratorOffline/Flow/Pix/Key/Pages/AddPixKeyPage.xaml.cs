using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class AddPixKeyPage : PopupPage
    {
        AddPixKeyViewModel _viewModel;

        public AddPixKeyPage(Models.PaymentMethods.Pix.PixKey pixKey = null, bool isContact = false)
        {
            InitializeComponent();

            BindingContext = _viewModel = new AddPixKeyViewModel(pixKey, isContact);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync().SafeFireAndForget((e) => e.SendToLog());
        }
    }
}