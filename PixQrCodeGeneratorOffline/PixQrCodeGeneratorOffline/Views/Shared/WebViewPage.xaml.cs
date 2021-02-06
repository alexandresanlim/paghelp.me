using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewPage : ContentPageWithNavBar
    {
        public static Uri CurrentUri { get; set; }

        public WebViewPage(Uri linkToShow, string title = "")
        {
            InitializeComponent();

            BindingContext = this;

            CurrentUri = linkToShow;

            Title = title;
        }

        protected override void OnAppearing()
        {
            Wv.Source = CurrentUri;

            Wv.Navigating += Wv_Navigating;
            Wv.Navigated += Wv_Navigated;

            base.OnAppearing();
        }

        private void Wv_Navigating(object sender, WebNavigatingEventArgs e)
        {
            UserDialogs.Instance.ShowLoading("");
        }

        private void Wv_Navigated(object sender, WebNavigatedEventArgs e)
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}