using PixQrCodeGeneratorOffline.Models.Repository;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Viewer;
using PixQrCodeGeneratorOffline.Models.Validation.Services;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer.Services;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Services.Interfaces;
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
            RegisterDependencyViewer();
            RegisterDependencyRepository();
            RegisterDependencyValidation();
        }

        private void RegisterDependencyViewer()
        {
            DependencyService.Register<IFeedViewerService, FeedViewerService>();
            DependencyService.Register<IPixKeyViewerService, PixKeyViewerService>();
            DependencyService.Register<IPixCobViewerService, PixCobViewerService>();
        }

        private void RegisterDependencyService()
        {
            DependencyService.Register<IFinancialInstitutionService, FinancialInstitutionService>();
            DependencyService.Register<IGuideService, GuideService>();
            DependencyService.Register<IPixKeyService, PixKeyService>();
            DependencyService.Register<IPixCobService, PixCobService>();
            DependencyService.Register<IPixPayloadService, PixPayloadService>();
            DependencyService.Register<IMaterialColorService, MaterialColorService>();
            DependencyService.Register<IPreferenceService, PreferenceService>();
            DependencyService.Register<IExternalActionService, ExternalActionService>();
            DependencyService.Register<IEventService, EventService>();
            DependencyService.Register<IFeedService, FeedService>();
        }

        private void RegisterDependencyRepository()
        {
            DependencyService.Register<IPixKeyRepository, PixKeyRepository>();
        }

        private void RegisterDependencyValidation()
        {
            DependencyService.Register<IFeedValidationService, FeedValidationService>();
        }

        protected override void OnStart()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            var service = DependencyService.Get<IStatusBar>();
            service?.SetByStyleListColor();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
