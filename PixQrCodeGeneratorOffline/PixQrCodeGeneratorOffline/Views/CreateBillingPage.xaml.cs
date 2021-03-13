using PixQrCodeGeneratorOffline.ViewModels;
using PixQrCodeGeneratorOffline.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateBillingPage : ContentPageWithNavBar
    {
        CreateBillingViewModel _createBillingViewModel;

        Models.PixKey _pixKey;

        public CreateBillingPage(Models.PixKey pixKey)
        { 
            InitializeComponent();

            _pixKey = pixKey;

            BindingContext = _createBillingViewModel = new CreateBillingViewModel();
        }

        protected override void OnAppearing()
        {
            _createBillingViewModel.LoadDataCommand.Execute(_pixKey);
        }

        //private void CustomEntry_Unfocused(object sender, FocusEventArgs e)
        //{
        //    _createBillingViewModel.CurrentPixKey.RaiseCob();
        //}
    }
}