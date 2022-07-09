using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class BenefitsPage : ContentPage
    {
        public BenefitsPage(bool hasKeys = false)
        {
            InitializeComponent();
            BindingContext = new BenefitsViewModel();
            xBtAddKey.IsVisible = !hasKeys;
        }
    }
}