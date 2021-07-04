using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionViewModel : ViewModelBase
    {
        public ICommand OpenPreferencesOptionsCommand => new Command(async () => await NavigateAsync(new OptionPreferencePage()));

        public ICommand OpenKeysOptionsCommand => new Command(async () => await NavigateAsync(new OptionKeyPage()));

        //private async Task OptionsPreferenceOpen()
        //{
        //    var preferences = new List<Acr.UserDialogs.ActionSheetOption>();

        //    if (PixKeyList != null && PixKeyList.Count > 0)
        //    {
        //        preferences.Add(new Acr.UserDialogs.ActionSheetOption(Preference.ShowInList ? "Exibir em carrossel" : "Exibir em lista", async () =>
        //        {
        //            _preferenceService.ChangeShowInList();
        //            await ReloadShowInList();
        //        }));
        //    }

        //    if (await CrossFingerprint.Current.IsAvailableAsync())
        //    {
        //        preferences.Add(new Acr.UserDialogs.ActionSheetOption((Preference.FingerPrint ? "Remover" : "Adicionar") + " autenticação biométrica", async () =>
        //        {
        //            await _preferenceService.ChangeFingerPrint();
        //        }));
        //    }

        //    if (preferences.Count.Equals(0))
        //    {
        //        DialogService.Toast("Nenhum preferência disponível para o seu dispositivo");
        //        return;
        //    }

        //    DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
        //    {
        //        Title = "Preferências",
        //        Options = preferences,
        //        Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
        //        {
        //            return;
        //        })
        //    });
        //}

        
    }
}
