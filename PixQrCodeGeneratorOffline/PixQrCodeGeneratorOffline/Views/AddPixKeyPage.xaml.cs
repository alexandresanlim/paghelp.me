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
    public partial class AddPixKeyPage : ContentPage
    {
        AddPixKeyViewModel _viewModel;
        //DashboardViewModel _dashboardViewModel;

        public AddPixKeyPage(DashboardViewModel dashboardVM, Models.PixKey pixKey = null)
        {
            InitializeComponent();

            //_dashboardViewModel = dbViewModel;
            BindingContext = _viewModel = new AddPixKeyViewModel(dashboardVM, pixKey);
        }

        protected override bool OnBackButtonPressed()
        {
            _viewModel.DashboardViewModel.SetStatusFromCurrentPixColor();

            return base.OnBackButtonPressed();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.SendBackButtonPressed();
        }
    }
}