using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class AddPixKeyPage : ContentPage
    {
        AddPixKeyViewModel _viewModel;

        public AddPixKeyPage(Models.PaymentMethods.Pix.PixKey pixKey = null, bool isContact = false)
        {
            InitializeComponent();

            BindingContext = _viewModel = new AddPixKeyViewModel(pixKey, isContact);
        }
    }
}