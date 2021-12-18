using PixQrCodeGeneratorOffline.ViewModels;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class BillingSaveListPage : ContentPage
    {
        public BillingSaveListPage(Models.PixKey pixKey = null)
        {
            InitializeComponent();
            BindingContext = new BillingSaveListViewModel(pixKey);
        }
    }
}