using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionPreferenceViewModel : ViewModelBase
    {
        public ICommand OptionStyleListCommand => new Command(() =>
        {
            var options = new List<Acr.UserDialogs.ActionSheetOption>
            {
                new Acr.UserDialogs.ActionSheetOption(Preference.ShowInList ? "Exibir em carrossel" : "Exibir em lista", async () =>
                {
                    _preferenceService.ChangeShowInList();
                    await DashboardVM.ReloadShowInList();
                    await NavigateToRootAsync();
                })
            };

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "Dashboard",
                Options = options,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });

        });

        public ICommand OptionFingerPrintCommand => new Command(async () => await _preferenceService.ChangeFingerPrint());

        public ICommand OptionPDVCommand => new Command(async () => await _preferenceService.ChangePDVMode());

        //private List<PixKey> CurrentPixKeyList { get; set; }
    }
}
