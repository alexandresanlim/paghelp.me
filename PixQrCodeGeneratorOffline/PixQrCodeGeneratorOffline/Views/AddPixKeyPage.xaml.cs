using PixQrCodeGeneratorOffline.ViewModels;
using System;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class AddPixKeyPage : ContentPage
    {
        AddPixKeyViewModel _viewModel;

        public AddPixKeyPage(Models.PaymentMethods.Pix.PixKey pixKey = null, bool isContact = false)
        {
            InitializeComponent();

            BindingContext = _viewModel = new AddPixKeyViewModel(pixKey, isContact);
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    _viewModel.BackButtonPressed();

        //    return base.OnBackButtonPressed();
        //}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.SendBackButtonPressed();
        }
    }
}