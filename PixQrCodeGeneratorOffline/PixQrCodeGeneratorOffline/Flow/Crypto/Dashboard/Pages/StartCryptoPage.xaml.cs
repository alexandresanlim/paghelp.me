
using PixQrCodeGeneratorOffline.ViewModels;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class StartCryptoPage : ContentPage
    {
        private readonly DashboardCryptoViewModel dashboardCryptoViewModel;

        public StartCryptoPage()
        {
            try
            {
                InitializeComponent();
                xMyKeysCrypto.IndicatorView = xIndicatorView;

                BindingContext = dashboardCryptoViewModel = new DashboardCryptoViewModel();
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}