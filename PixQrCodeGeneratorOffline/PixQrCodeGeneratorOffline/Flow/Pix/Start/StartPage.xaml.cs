using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class StartPage : ContentPage
    {
        DashboardViewModel _viewModel;

        public StartPage()
        {
                InitializeComponent();
                xMyKeys.IndicatorView = xIndicatorView;

                BindingContext = _viewModel = new DashboardViewModel();
           
        }
    }
}