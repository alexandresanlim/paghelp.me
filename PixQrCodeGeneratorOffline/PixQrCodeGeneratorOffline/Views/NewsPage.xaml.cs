using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class NewsPage : ContentPage
    {
        NewsViewModel _newsViewModel;

        public NewsPage()
        {
            BindingContext = _newsViewModel = new NewsViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            _newsViewModel.Navigating();
        }
    }
}