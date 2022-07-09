using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class CreateBillingPage : ContentPage
    {
        CreateBillingViewModel _createBillingViewModel;

        public CreateBillingPage()
        { 
            InitializeComponent();

            BindingContext = _createBillingViewModel = new CreateBillingViewModel();
        }
    }
}