using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Sobre";
        }

        public ICommand OpenGithubCommand => new Command(async () =>
        {
            await Xamarin.Essentials.Browser.OpenAsync("https://github.com/alexandresanlim/PixQrCodeGeneratorOffline");
        });

        public ICommand SupportCommand => new Command(async () =>
        {
            var options = new List<Acr.UserDialogs.ActionSheetOption>
            {
                new Acr.UserDialogs.ActionSheetOption("Avaliar na " + App.Info.StoreNameByDeviceInfo, async () =>
                {
                    await App.OpenAppInStore();
                }),
                new Acr.UserDialogs.ActionSheetOption("Compartilhar", async () =>
                {
                    await ShareText(App.Info.StoreTextToShare);
                }),
                new Acr.UserDialogs.ActionSheetOption("Copiar código copia e cola pix para doação", async () =>
                {
                    await CopyText("00020126580014br.gov.bcb.pix0136bee05743-4291-4f3c-9259-595df1307ba1520400005303986540510.005802BR5914Alexandre Lima6019Presidente Prudente62180514Um-Id-Qualquer6304D475", "Código copiado com sucesso!");
                })
            };

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "Selecione uma opção",
                Options = options,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        });
    }
}