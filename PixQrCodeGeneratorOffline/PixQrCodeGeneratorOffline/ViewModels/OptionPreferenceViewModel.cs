using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Services;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionPreferenceViewModel : ViewModelBase
    {
        public void LoadData()
        {
            IsPreferenceFingerPrint = Preference.FingerPrint;
            IsPreferenceNews = Preference.ShowNews;
            IsPreferncePdvMode = Preference.IsPDVMode;
            IsThemeDark = Preference.ThemeIsDark;
            IsCryptoAble = Preference.CryptoAble;
            LoadThemeIcon();
        }

        public async Task OptionFingerPrint()
        {
            var success = await _preferenceService.ChangeFingerPrint();

            if (success)
            {
                LoadData();
            }

            else
            {
                IsPreferenceFingerPrint = false;
            }

        }

        public void OptionPDV()
        {
            _preferenceService.ChangePDVMode();
            LoadData();
        }

        public void OptionShowNews()
        {
            _preferenceService.ChangeShowNewsMode();
            LoadData();
            //await DashboardVM.LoadNews();
        }

        public void OptionTheme()
        {
            _preferenceService.ChangeTheme();
            LoadData();
        }

        public void ChangeCrypto()
        {
            _preferenceService.ChangeCrypto();
            LoadData();
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
