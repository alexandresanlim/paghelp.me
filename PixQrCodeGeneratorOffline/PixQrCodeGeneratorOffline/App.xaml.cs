using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            var service = DependencyService.Get<IStatusBar>();
            service?.SetStatusBarColor(App.ThemeColors.Primary);

            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
