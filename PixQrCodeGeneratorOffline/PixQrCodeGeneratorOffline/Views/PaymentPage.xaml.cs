using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.ViewModels;
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
    public partial class PaymentPage : Shared.ContentPageWithNavBar
    {
        PaymentViewModel _paymentViewModel;

        PixPayload _pixPaylod;

        public PaymentPage(PixPayload paylod)
        {
            InitializeComponent();

            _pixPaylod = paylod;

            BindingContext = _paymentViewModel = new PaymentViewModel();
        }

        protected override void OnAppearing()
        {
            _paymentViewModel.LoadDataCommand.Execute(_pixPaylod);
        }

        //protected override void OnDisappearing()
        //{
        //    _paymentViewModel.CurrentPixKey.Value = "";
        //    _paymentViewModel.CurrentPixKey.RaiseCob();

        //    base.OnDisappearing();
        //}
    }
}