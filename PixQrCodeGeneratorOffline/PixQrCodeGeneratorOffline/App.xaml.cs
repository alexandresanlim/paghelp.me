using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Interfaces;
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

            RegisterDependency();

            MainPage = new AppShell();
        }

        private void RegisterDependency()
        {
            RegisterDependencyModel();
            RegisterDependencyService();
        }

        private void RegisterDependencyService()
        {
            DependencyService.Register<IFeedViewerService, FeedViewerService>();
        }

        private void RegisterDependencyModel()
        {
            DependencyService.Register<IFeedViewer, FeedViewer>();
        }

        protected override void OnStart()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            var service = DependencyService.Get<IStatusBar>();
            service?.SetStatusBarColor(!PreferenceService.ShowInList ? ThemeColors.Primary : ThemeColors.PrimaryDark);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
