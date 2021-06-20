using PixQrCodeGeneratorOffline.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionPreferenceViewModel : BaseViewModel
    {
        public ICommand OptionFingerPrintCommand => new Command(() =>
        {
            var options = new List<Acr.UserDialogs.ActionSheetOption>
            {
                new Acr.UserDialogs.ActionSheetOption((Preference.FingerPrint ? "Remover" : "Adicionar") + " autenticação biométrica", async () =>
                {
                    await _preferenceService.ChangeFingerPrint();
                })
            };

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "Proteção por biometria",
                Options = options,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });

        });

        //private List<PixKey> CurrentPixKeyList { get; set; }
    }
}
