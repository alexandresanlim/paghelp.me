using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
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

            //App.StatusBarService.SetStatusBarColor(paylod.PixKey.FinancialInstitution.Institution.MaterialColor.Primary);

            BindingContext = _paymentViewModel = new PaymentViewModel();
        }

        protected override void OnAppearing()
        {
            _paymentViewModel.LoadDataCommand.Execute(_pixPaylod);

            if (!(_paymentViewModel?.CurrentPixPaylod?.Id > 0) && _paymentViewModel?.CurrentPixPaylod?.PixCob != null && _paymentViewModel.CurrentPixPaylod.PixCob.Validation.HasValue)
                ToolbarItems.Add(new ToolbarItem
                {
                    Command = _paymentViewModel.SaveCommand,
                });
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    //ReloadStatusBar();
        //    Shell.Current.SendBackButtonPressed();
        //}

        //protected override bool OnBackButtonPressed()
        //{
        //    //ReloadStatusBar();
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