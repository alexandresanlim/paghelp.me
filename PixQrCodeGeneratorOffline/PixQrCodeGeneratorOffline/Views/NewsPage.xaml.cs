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
    public partial class NewsPage : ContentPage
    {
        NewsViewModel _newsViewModel;

        public NewsPage()
        {
            BindingContext = _newsViewModel = new NewsViewModel();

            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await _newsViewModel.Navigating();
        }
    }
}