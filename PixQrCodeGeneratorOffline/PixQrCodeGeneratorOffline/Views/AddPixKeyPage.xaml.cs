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
        DashboardViewModel _dashboardViewModel;

        public AddPixKeyPage(DashboardViewModel dbViewModel, Models.PixKey pixKey = null)
        {
            InitializeComponent();

            _dashboardViewModel = dbViewModel;
            BindingContext = _viewModel = new AddPixKeyViewModel(dbViewModel, pixKey);
        }

        protected override bool OnBackButtonPressed()
        {
            _dashboardViewModel.SetStatusFromCurrentPixColor();

            return base.OnBackButtonPressed();
        }
    }
}