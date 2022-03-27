using Acr.UserDialogs;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Flow.Core.Security;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using PixQrCodeGeneratorOffline.ViewModels;
using PixQrCodeGeneratorOffline.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Base.ViewModels
{
    public class ViewModelBase : NotifyObjectBase
    {
        protected readonly IFinancialInstitutionService _financialInstitutionService;

        protected readonly IFinancialInstitutionCryptoService _financialInstitutionCryptoService;

        protected readonly IPixKeyService _pixKeyService;

        protected readonly ICryptoKeyService _cryptoKeyService;

        //public readonly IStatusBar _statusBarService;

        protected readonly IMaterialColorService _materialColorService;

        protected readonly IPixPayloadService _pixPayloadService;

        protected readonly IPreferenceService _preferenceService;

        protected readonly IExternalActionService _externalActionService;

        protected readonly IEventService _eventService;

        protected readonly IFeedService _feedService;

        protected readonly IStatusBar _statusBar;

        protected readonly IPixKeyViewerService _pixKeyViewerService;

        protected readonly IPixKeyCommand _pixKeyCommand;

        public ViewModelBase()
        {
            _financialInstitutionService = DependencyService.Get<IFinancialInstitutionService>();
            _financialInstitutionCryptoService = DependencyService.Get<IFinancialInstitutionCryptoService>();
            _pixKeyService = DependencyService.Get<IPixKeyService>();
            _cryptoKeyService = DependencyService.Get<ICryptoKeyService>();
            //_statusBarService = DependencyService.Get<IStatusBar>();
            _materialColorService = DependencyService.Get<IMaterialColorService>();
            _pixPayloadService = DependencyService.Get<IPixPayloadService>();
            _preferenceService = DependencyService.Get<IPreferenceService>();
            _externalActionService = DependencyService.Get<IExternalActionService>();
            _eventService = DependencyService.Get<IEventService>();
            _feedService = DependencyService.Get<IFeedService>();
            _statusBar = DependencyService.Get<IStatusBar>();
            _pixKeyViewerService = DependencyService.Get<IPixKeyViewerService>();
            _pixKeyCommand = DependencyService.Get<IPixKeyCommand>();

            ShowAds = false;

            //Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        }

        public static DashboardViewModel DashboardVM { get; set; }

        public static DashboardCryptoViewModel DashboardCryptoVM { get; set; }

        public static DashboardContactViewModel DashboardContactVM { get; set; }

        //private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        //{
        //    ReloadAppColorIfShowInListStyle();
        //}

        //public void ReloadAppColorIfShowInListStyle()
        //{
        //    App.LoadTheme();
        //}

        protected IUserDialogs DialogService => UserDialogs.Instance;

        #region Loading

        bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            private set { SetProperty(ref isLoading, value); }
        }

        public void SetIsLoading(bool isLoading = true, string title = "")
        {
            IsLoading = isLoading;

            if (IsLoading)
                DialogService.ShowLoading(title);

            else
                DialogService.HideLoading();
        }

        #endregion

        public async Task LoadAuthenticationPage(Action execute)
        {
            var isVisibleFingerPrint = Preference.FingerPrint && await CrossFingerprint.Current.IsAvailableAsync();

            if(isVisibleFingerPrint)
            {
                await Shell.Current.Navigation.PushPopupAsync(new AuthenticationPage(execute));
            }
            else
                execute.Invoke();
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        #region Navigate

        public Command NavigateBackCommand => new Command(() =>
        {
            NavigateBack();
        });

        public Command NavigateToRootCommand => new Command(async () =>
        {
            await NavigateToRootAsync();
        });

        public async Task NavigateAsync(Page page)
        {
            await Shell.Current.Navigation.PushAsync(page, true);
        }

        public async Task NavigateModalAsync(Page page)
        {
            await Shell.Current.Navigation.PushModalAsync(page, true);
        }

        public void NavigateBack()
        {
            Shell.Current.SendBackButtonPressed();
        }

        public async Task NavigateBackPopupAsync()
        {
            await Shell.Current.Navigation.PopPopupAsync();
        }

        public async Task NavigateToRootAsync()
        {
            await Shell.Current.Navigation.PopToRootAsync(true);
        }

        #endregion

        public async Task NavigateToLikingPage() => await NavigateAsync(new LikingPage());

        public async Task WaitAndExecute(int milisec, Action actionToExecute) 
        { 
            await Task.Delay(milisec); actionToExecute(); 
        }

        public ICommand CloseAdsCommand => new Command(() =>
        {
            ShowAds = false;
        });

        private bool _showAds;
        public bool ShowAds
        {
            get => _showAds;
            set => SetProperty(ref _showAds, value);
        }

        private bool isBusy = true;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        string title = string.Empty;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private MaterialColor _currentStyleFromKey;
        public MaterialColor CurrentStyleFromKey
        {
            set => SetProperty(ref _currentStyleFromKey, value);
            get => _currentStyleFromKey;
        }
    }
}
