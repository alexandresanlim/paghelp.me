using PixQrCodeGeneratorOffline.ViewModels;
using PixQrCodeGeneratorOffline.Views.Shared;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel _aboutViewModel;

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = _aboutViewModel = new AboutViewModel();
        }

        protected override void OnAppearing()
        {
            lbVersion.Text = App.Info.VersionString;
            //lbDate.Text = App.Info.Date;

            base.OnAppearing();
        }
    }
}