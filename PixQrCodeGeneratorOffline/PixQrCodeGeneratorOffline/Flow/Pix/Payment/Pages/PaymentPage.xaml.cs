using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Linq;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class PaymentPage : PopupPage
    {
        PaymentViewModel _paymentViewModel;

        public PaymentPage(PayloadBase paylod)
        {
            InitializeComponent();
            BindingContext = _paymentViewModel = new PaymentViewModel(paylod);
        }

        protected override void OnAppearing()
        {
            //_paymentViewModel.paymentCanceled = false;
            //_paymentViewModel.LoadDataCommand.Execute(_pixPaylod);

            //SetStatusBarColor(_paymentViewModel.CurrentInfo.Color.PrimaryDark);

            //if (!(_paymentViewModel?.CurrentPixPaylod?.Id > 0) && _paymentViewModel?.CurrentPixPaylod?.PixCob != null && _paymentViewModel.CurrentPixPaylod.PixCob.HasValue())
            //    btnSave.IsVisible = true;
        }

        protected override void OnDisappearing()
        {
            //_paymentViewModel.paymentCanceled = true;

            _paymentViewModel.waitingPaymentTokenSource.Cancel();
        }

        protected override bool OnBackButtonPressed()
        {
            //_paymentViewModel.waitingPaymentTokenSource.Cancel();

            if (PopupNavigation.Instance.PopupStack.Any())
            {
                Shell.Current.Navigation.PopPopupAsync().SafeFireAndForget(x => x.SendToLog());
            }

            return base.OnBackButtonPressed();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            //_paymentViewModel.paymentCanceled = true;

            _paymentViewModel.waitingPaymentTokenSource.Cancel();
            //SetStatusBarColor(App.ThemeColors.PrimaryDark);
            Shell.Current.Navigation.PopPopupAsync().SafeFireAndForget(x => x.SendToLog());
        }
    }
}