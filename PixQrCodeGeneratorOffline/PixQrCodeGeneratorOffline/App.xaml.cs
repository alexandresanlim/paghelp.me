using PixQrCodeGeneratorOffline.Models.Services;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using System.Globalization;
using Xamarin.Forms;

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
            RegisterDependencyService();
            RegisterDependencyRepository();
        }

        private void RegisterDependencyService()
        {
            DependencyService.Register<IFeedViewerService, FeedViewerService>();
            DependencyService.Register<IPixKeyViewerService, PixKeyViewerService>();

            DependencyService.Register<IFinancialInstitutionService, FinancialInstitutionService>();
            DependencyService.Register<IGuideService, GuideService>();
            DependencyService.Register<IPixKeyService, PixKeyService>();
            DependencyService.Register<IPixCobService, PixCobService>();
            DependencyService.Register<IPixPayloadService, PixPayloadService>();
        }

        private void RegisterDependencyRepository()
        {

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
