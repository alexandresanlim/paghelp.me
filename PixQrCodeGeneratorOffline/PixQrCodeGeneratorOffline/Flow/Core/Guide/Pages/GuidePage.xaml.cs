using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class GuidePage : ContentPage
    {
        GuideViewModel _guideViewModel;

        public GuidePage()
        {
            InitializeComponent();

            BindingContext = _guideViewModel = new GuideViewModel();
        }
    }
}