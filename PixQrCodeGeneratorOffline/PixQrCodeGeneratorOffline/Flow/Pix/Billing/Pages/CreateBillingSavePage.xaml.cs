using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class CreateBillingSavePage : ContentPage
    {
        public CreateBillingSavePage()
        {
            BindingContext = new CreateBillingSaveViewModel();

            InitializeComponent();
        }
    }
}