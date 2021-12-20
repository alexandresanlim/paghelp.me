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

            BindingContext = _viewModel = new DashboardViewModel();
        }

        //protected override void OnAppearing()
        //{
        //    _viewModel.LoadDataCommand.ExecuteAsync().SafeFireAndForget();
        //}
    }
}