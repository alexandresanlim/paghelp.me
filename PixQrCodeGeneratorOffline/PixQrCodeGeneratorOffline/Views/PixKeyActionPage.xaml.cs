using PixQrCodeGeneratorOffline.Models;
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
    public partial class PixKeyActionPage : ContentPageWithNavBar
    {
        private PixKeyActionViewModel _pixKeyActionViewModel;

        public PixKeyActionPage(DashboardViewModel dashboardVM, PixKey pixKey)
        {
            InitializeComponent();

            BindingContext = _pixKeyActionViewModel = new PixKeyActionViewModel(dashboardVM, pixKey);
        }

        protected override void OnAppearing()
        {
            _pixKeyActionViewModel.LoadData();

            base.OnAppearing();
        }
    }
}