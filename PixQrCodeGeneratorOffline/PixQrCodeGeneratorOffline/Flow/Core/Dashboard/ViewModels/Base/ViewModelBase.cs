using Acr.UserDialogs;
using AsyncAwaitBestPractices.MVVM;
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
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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
            var isVisibleFingerPrint = Preference.FingerPrint && await CrossFingerprint.Current.IsAvailableAsync().ConfigureAwait(false);

            if(isVisibleFingerPrint)
            {
                await Shell.Current.Navigation.PushPopupAsync(new AuthenticationPage(execute)).ConfigureAwait(false);
            }
            else
                execute.Invoke();
        }

        public Task DisplayAlert(string title, string message, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public void ShowToastErrorMessage()
        {
            DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");
        }

        #region Navigate

        public Command NavigateBackCommand => new Command(NavigateBack);

        public IAsyncCommand NavigateToRootCommand => new AsyncCommand(NavigateToRootAsync);

        public Task NavigateAsync(Page page)
        {
            return Shell.Current.Navigation.PushAsync(page, true);
        }

        public Task NavigatePopupAsync(PopupPage page)
        {
            return Shell.Current.Navigation.PushPopupAsync(page, true);
        }

        public Task NavigateModalAsync(Page page)
        {
            return Shell.Current.Navigation.PushModalAsync(page, true);
        }

        public void NavigateBack()
        {
            Shell.Current.SendBackButtonPressed();
        }

        public Task NavigateBackPopupAsync()
        {
            return Shell.Current.Navigation.PopPopupAsync();
        }

        public Task NavigateToRootAsync()
        {
            return Shell.Current.Navigation.PopToRootAsync(true);
        }

        #endregion

        public Task NavigateToLikingPage()
        {
            return NavigatePopupAsync(new LikingPage());
        }

        public async Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await Task.Delay(milisec).ConfigureAwait(false); 

            MainThread.BeginInvokeOnMainThread(() => actionToExecute.Invoke());
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
