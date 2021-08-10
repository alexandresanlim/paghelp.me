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
    public partial class AddPixKeyPage : ContentPageWithNavBar
    {
        AddPixKeyViewModel _viewModel;

        public AddPixKeyPage(Models.PixKey pixKey = null, bool isContact = false)
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