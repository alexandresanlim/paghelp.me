using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class CreateBillingPage : ContentPage
    {
        CreateBillingViewModel _createBillingViewModel;

        public CreateBillingPage(PixKey pixKey)
        { 
            InitializeComponent();

            BindingContext = _createBillingViewModel = new CreateBillingViewModel();
            _createBillingViewModel.LoadDataCommand.Execute(pixKey);

        }
    }
}