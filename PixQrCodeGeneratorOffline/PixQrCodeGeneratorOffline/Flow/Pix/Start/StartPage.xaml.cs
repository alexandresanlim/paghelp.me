using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Templates;
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

        private async void TemplateTitlePanel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is ContentView && e.PropertyName.Equals(nameof(BackgroundColor)))
            {
                await tpMyKeys.RunOpacityAnimationAsync().ConfigureAwait(false);
            }
        }
    }
}