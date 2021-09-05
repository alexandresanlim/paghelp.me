using PixQrCodeGeneratorOffline.Base.ViewModels;
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

        public async Task OptionPDV()
        {
            await _preferenceService.ChangePDVMode();
            LoadData();
        }

        public async Task OptionShowNews()
        {
            await _preferenceService.ChangeShowNewsMode();
            LoadData();
            await DashboardVM.LoadNews();
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
    }
}
