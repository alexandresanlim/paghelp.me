using Acr.UserDialogs;
using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.ViewModels;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views.Shared
{
    public partial class WebViewPage : ContentPage
    {
        WebViewViewModel _webViewVM;

        public WebViewPage(Uri linkToShow, string title = "")
        {
            InitializeComponent();

            BindingContext = _webViewVM = new WebViewViewModel(linkToShow);

            Title = title;
        }

        protected override void OnAppearing()
        {
            Wv.Navigating += Wv_Navigating;

            //Wv.Navigated += Wv_Navigated;

            base.OnAppearing();
        }

        private void Wv_Navigating(object sender, WebNavigatingEventArgs e)
        {
            UserDialogs.Instance.ShowLoading("");

            Task.Delay(2000).SafeFireAndForget();

            UserDialogs.Instance.HideLoading();
        }

        //private void Wv_Navigated(object sender, WebNavigatedEventArgs e)
        //{
            
        //}
    }
}