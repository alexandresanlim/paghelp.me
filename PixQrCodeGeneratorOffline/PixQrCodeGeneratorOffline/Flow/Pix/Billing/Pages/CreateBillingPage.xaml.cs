using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class CreateBillingPage : ContentPage
    {
        CreateBillingViewModel _createBillingViewModel;

        public CreateBillingPage(PixKey pixKey)
        {
            InitializeComponent();

            var color = pixKey?.FinancialInstitution?.Institution?.MaterialColor?.PrimaryDark;

            if (color != null)
                SetStatusBarColor(color.Value);

            //pixPayload.PixKey?.FinancialInstitution?.Institution?.MaterialColor;
            BindingContext = _createBillingViewModel = new CreateBillingViewModel();
            _createBillingViewModel.LoadDataCommand.Execute(pixKey);

        }

        private void SetStatusBarColor(Color color)
        {
            App.StatusBarService.SetStatusBarColor(color);
        }
    }
}