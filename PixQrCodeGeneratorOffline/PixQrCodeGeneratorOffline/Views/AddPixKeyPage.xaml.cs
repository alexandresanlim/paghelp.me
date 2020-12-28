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
            //if (!_viewModel.IsEdit)

            //_dashboardViewModel.LoadCurrentPixKey((_dashboardViewModel.PixKeyList.FirstOrDefault(x => x.Id == _dashboardViewModel.CurrentPixKey.Id)));

            _dashboardViewModel.CurrentPixKey = _viewModel.IsEdit ? AddPixKeyViewModel.OriginPixKey : _dashboardViewModel.CurrentPixKey;

            _dashboardViewModel.LoadCurrentPixKey(_dashboardViewModel.CurrentPixKey);

            return base.OnBackButtonPressed();
        }
    }
}