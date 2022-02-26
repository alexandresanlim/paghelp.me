using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class PaymentPage : ContentPage
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
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Salvar",
                    Command = _paymentViewModel.SaveCommand,
                });
            }

            App.StatusBarService.SetStatusBarColor(_paymentViewModel.CurrentInfo.Color.PrimaryDark);
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    //ReloadStatusBar();
        //    Shell.Current.SendBackButtonPressed();
        //}

        //protected override bool OnBackButtonPressed()
        //{
        //    ReloadStatusBar();
        //    return base.OnBackButtonPressed();
        //}

        //private void ReloadStatusBar()
        //{
        //    App.StatusBarService.SetStatusBarColor(App.ThemeColors.PrimaryDark);
        //}

        //protected override void OnDisappearing()
        //{
        //    _paymentViewModel.CurrentPixKey.Value = "";
        //    _paymentViewModel.CurrentPixKey.RaiseCob();

        //    base.OnDisappearing();
        //}
    }
}