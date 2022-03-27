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

            if (!(_paymentViewModel?.CurrentPixPaylod?.Id > 0) && _paymentViewModel?.CurrentPixPaylod?.PixCob != null && _paymentViewModel.CurrentPixPaylod.PixCob.HasValue())
            {
                btnSave.IsVisible = true;
                //ToolbarItems.Add(new ToolbarItem
                //{
                //    Text = "Salvar",
                //    Command = _paymentViewModel.SaveCommand,
                //});
            }

           // App.StatusBarService.SetStatusBarColor(_paymentViewModel.CurrentInfo.Color.PrimaryDark);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Shell.Current.Navigation.PopPopupAsync().SafeFireAndForget(x => x.SendToLog());
        }
    }
}