using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class DashboardContactPage : ContentPage
    {
        DashboardContactViewModel _dashboardContactViewModel;

        public DashboardContactPage()
        {
            BindingContext = _dashboardContactViewModel = new DashboardContactViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            _dashboardContactViewModel.LoadDataCommand.ExecuteAsync().SafeFireAndForget();
        }
    }
}