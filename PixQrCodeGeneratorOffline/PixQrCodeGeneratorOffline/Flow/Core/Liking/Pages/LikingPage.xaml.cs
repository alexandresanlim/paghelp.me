
using PixQrCodeGeneratorOffline.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class LikingPage : PopupPage
    {
        LikingViewModel _linkingViewModel;

        public LikingPage()
        {
            InitializeComponent();

            BindingContext = _linkingViewModel = new LikingViewModel();
        }
    }
}