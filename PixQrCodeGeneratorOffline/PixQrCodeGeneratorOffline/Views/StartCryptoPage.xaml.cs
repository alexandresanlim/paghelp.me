
using PixQrCodeGeneratorOffline.ViewModels;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class StartCryptoPage : ContentPage
    {
        private readonly DashboardCryptoViewModel dashboardCryptoViewModel;

        public StartCryptoPage()
        {
            InitializeComponent();
            BindingContext = dashboardCryptoViewModel = new DashboardCryptoViewModel();
        }
    }
}