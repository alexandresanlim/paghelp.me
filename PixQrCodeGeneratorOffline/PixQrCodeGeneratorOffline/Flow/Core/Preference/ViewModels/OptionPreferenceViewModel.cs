using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Services;
using Plugin.Fingerprint;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionPreferenceViewModel : ViewModelBase
    {
        public async Task LoadData()
        {
            IsPreferenceFingerPrint = Preference.FingerPrint && await CrossFingerprint.Current.IsAvailableAsync();
            IsPreferenceNews = Preference.ShowNews;
            IsPreferncePdvMode = Preference.IsPDVMode;
            IsThemeDark = Preference.ThemeIsDark;
            IsCryptoAble = Preference.CryptoAble;
            LoadThemeIcon();
        }

        public async Task OptionFingerPrint()
        {
            await _preferenceService.RequireAuthenticationToAction(async () =>
            {
                var success = await _preferenceService.ChangeFingerPrint();

                if (success)
                {
                    await LoadData();
                }

                else
                {
                    IsPreferenceFingerPrint = false;
                }
            }, false);
        }

        public async Task OptionPDV()
        {
            _preferenceService.ChangePDVMode();
            await LoadData();
        }

        public async Task OptionShowNews()
        {
            _preferenceService.ChangeShowNewsMode();
            await LoadData();
            //await DashboardVM.LoadNews();
        }

        public async Task OptionTheme()
        {
            _preferenceService.ChangeTheme();
            await LoadData();
        }

        public async Task ChangeCrypto()
        {
            _preferenceService.ChangeCrypto();
            await LoadData();
        }

        private void LoadThemeIcon()
        {
            ThemeIcon = Preference.ThemeIsDark ? FontAwesomeSolid.Moon : FontAwesomeSolid.Sun;
        }


        private bool _isPreferenceNews;
        public bool IsPreferenceNews
        {
            set => SetProperty(ref _isPreferenceNews, value);
            get => _isPreferenceNews;
        }

        private bool _isPreferenceFingerPrint;
        public bool IsPreferenceFingerPrint
        {
            set => SetProperty(ref _isPreferenceFingerPrint, value);
            get => _isPreferenceFingerPrint;
        }

        private bool _isPreferncePdvMode;
        public bool IsPreferncePdvMode
        {
            set => SetProperty(ref _isPreferncePdvMode, value);
            get => _isPreferncePdvMode;
        }

        private bool _isThemeDark;
        public bool IsThemeDark
        {
            set => SetProperty(ref _isThemeDark, value);
            get => _isThemeDark;
        }

        private string _themeIcon;
        public string ThemeIcon
        {
            set => SetProperty(ref _themeIcon, value);
            get => _themeIcon;
        }

        private bool _isCryptoAble;
        public bool IsCryptoAble
        {
            set => SetProperty(ref _isCryptoAble, value);
            get => _isCryptoAble;
        }
    }
}
