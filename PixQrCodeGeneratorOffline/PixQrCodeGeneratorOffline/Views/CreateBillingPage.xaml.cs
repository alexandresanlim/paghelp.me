using PixQrCodeGeneratorOffline.ViewModels;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class CreateBillingPage : ContentPage
    {
        CreateBillingViewModel _createBillingViewModel;

        //Models.PixKey _pixKey;

        public CreateBillingPage()
        { 
            InitializeComponent();

            //_pixKey = pixKey;

            BindingContext = _createBillingViewModel = new CreateBillingViewModel();
        }

        //protected override void OnAppearing()
        //{
        //    _createBillingViewModel.LoadDataCommand.Execute(_pixKey);
        //}

        //private void CustomEntry_Unfocused(object sender, FocusEventArgs e)
        //{
        //    _createBillingViewModel.CurrentPixKey.RaiseCob();
        //}
    }
}