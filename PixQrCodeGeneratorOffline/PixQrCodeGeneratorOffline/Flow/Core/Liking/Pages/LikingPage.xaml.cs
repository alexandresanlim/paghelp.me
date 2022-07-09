
using PixQrCodeGeneratorOffline.ViewModels;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class LikingPage : ContentPage
    {
        LikingViewModel _linkingViewModel;

        public LikingPage()
        {
            InitializeComponent();

            BindingContext = _linkingViewModel = new LikingViewModel();
        }
    }
}