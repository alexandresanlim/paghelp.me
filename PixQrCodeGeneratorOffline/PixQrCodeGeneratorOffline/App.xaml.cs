using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.Repository;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using PixQrCodeGeneratorOffline.Models.Repository.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Repository.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services;
using PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services.Interfaces;
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
            RegisterCommandDependency();
        }

        private void RegisterDependencyViewer()
        {
            DependencyService.Register<IFeedViewerService, FeedViewerService>();
            DependencyService.Register<IPixKeyViewerService, PixKeyViewerService>();
            DependencyService.Register<IPixCobViewerService, PixCobViewerService>();
            DependencyService.Register<ICryptoKeyViewerService, CryptoKeyViewerService>();
            DependencyService.Register<ICryptoCobViewerService, CryptoCobViewerService>();
        }

        private void RegisterDependencyService()
        {
            DependencyService.Register<IFinancialInstitutionService, FinancialInstitutionService>();
            DependencyService.Register<IFinancialInstitutionCryptoService, FinancialInstitutionCryptoService>();
            DependencyService.Register<IGuideService, GuideService>();
            DependencyService.Register<IPixKeyService, PixKeyService>();
            DependencyService.Register<ICryptoKeyService, CryptoKeyService>();
            DependencyService.Register<IPixCobService, PixCobService>();
            DependencyService.Register<IPixPayloadService, PixPayloadService>();
            DependencyService.Register<ICryptoPayloadService, CryptoPayloadService>();
            DependencyService.Register<IMaterialColorService, MaterialColorService>();
            DependencyService.Register<IPreferenceService, PreferenceService>();
            DependencyService.Register<IExternalActionService, ExternalActionService>();
            DependencyService.Register<IEventService, EventService>();
            DependencyService.Register<IFeedService, FeedService>();
        }

        private void RegisterDependencyRepository()
        {
            DependencyService.Register<IPixKeyRepository, PixKeyRepository>();
            DependencyService.Register<ICryptoKeyRepository, CryptoKeyRepository>();
            DependencyService.Register<IPixPayloadRepository, PixPayloadRepository>();
        }

        private void RegisterCommandDependency()
        {
            DependencyService.Register<IPixKeyCommand, PixKeyCommand>();
            DependencyService.Register<ICryptoKeyCommand, CryptoKeyCommand>();
            DependencyService.Register<IPixPayloadCommand, PixPayloadCommand>();
            DependencyService.Register<IPayloadCommandBase, PayloadCommandBase>();
            DependencyService.Register<ICryptoPayloadCommand, CryptoPayloadCommand>();
            DependencyService.Register<IFeedCommand, FeedCommand>();
            DependencyService.Register<ICustomAsyncCommand, CustomAsyncCommand>();
        }

        protected override void OnStart()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            LoadPtBrCultureInfo();
            LoadChangeAreYouLikingAppMsgCount();
            //LoadStatusBar();
            LoadPDVMode();
            LoadStyle();
        }

        private void LoadPtBrCultureInfo()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        }

        //private void LoadStatusBar()
        //{
        //    var service = DependencyService.Get<IStatusBar>();
        //    service?.SetByStyleListColor();
        //}

        private void LoadPDVMode()
        {
            var service = DependencyService.Get<IPDVMode>();
            service?.SetPDVMode();
        }

        private void LoadStyle()
        {
            App.LoadTheme();
        }

        private void LoadChangeAreYouLikingAppMsgCount()
        {
            var service = DependencyService.Get<IPreferenceService>();
            service.ChangeAreYouLikingAppMsgCount();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
