using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionPreferenceViewModel : ViewModelBase
    {
        //public ICommand OptionStyleListCommand => new Command(() =>
        //{
        //    var options = new List<Acr.UserDialogs.ActionSheetOption>
        //    {
        //        new Acr.UserDialogs.ActionSheetOption(Preference.ShowInList ? "Exibir em carrossel" : "Exibir em lista", async () =>
        //        {
        //            _preferenceService.ChangeShowInList();
        //            await DashboardVM.ReloadShowInList();
        //            await NavigateToRootAsync();
        //        })
        //    };

        //    DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
        //    {
        //        Title = "Dashboard",
        //        Options = options,
        //        Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
        //        {
        //            return;
        //        })
        //    });

        //});

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

        //private List<PixKey> CurrentPixKeyList { get; set; }
    }
}
