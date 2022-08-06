using PixQrCodeGeneratorOffline.ViewModels;
using Rg.Plugins.Popup.Pages;

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
    }
}