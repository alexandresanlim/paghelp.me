using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class PaymentPage : PopupPage
    {
        PaymentViewModel _paymentViewModel;

        PayloadBase _pixPaylod;

        public PaymentPage(PayloadBase paylod)
        {
            InitializeComponent();

            _pixPaylod = paylod;

            BindingContext = _paymentViewModel = new PaymentViewModel();
        }

        protected override void OnAppearing()
        {
            _paymentViewModel.LoadDataCommand.Execute(_pixPaylod);

            //SetStatusBarColor(_paymentViewModel.CurrentInfo.Color.PrimaryDark);

            //if (!(_paymentViewModel?.CurrentPixPaylod?.Id > 0) && _paymentViewModel?.CurrentPixPaylod?.PixCob != null && _paymentViewModel.CurrentPixPaylod.PixCob.HasValue())
            //    btnSave.IsVisible = true;
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            //SetStatusBarColor(App.ThemeColors.PrimaryDark);
            Shell.Current.Navigation.PopPopupAsync().SafeFireAndForget(x => x.SendToLog());
        }

        private void SetStatusBarColor(Color color)
        {
            App.StatusBarService.SetStatusBarColor(color);
        }
    }
}