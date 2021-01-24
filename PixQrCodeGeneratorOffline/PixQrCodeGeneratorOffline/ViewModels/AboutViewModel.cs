using PixQrCodeGeneratorOffline.Extention;
using System;
using System.Collections.Generic;
using System.Windows.Input;
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
            try
            {
                SetIsLoading(true);

                await Xamarin.Essentials.Browser.OpenAsync("https://github.com/pixqrcodegeneratoroffline/PixQrCodeGeneratorOffline");
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Abriu repositório Github");

                SetIsLoading(false);
            }
        });

        public ICommand SupportCommand => new Command(async () =>
        {
            try
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
                        await CopyText("00020126760014br.gov.bcb.pix0136bee05743-4291-4f3c-9259-595df1307ba10214Doação PIX OFF5204000053039865802BR5909Alexandre6008Curitiba62200516PIXOFFe2d72825e26304208D", "Código copiado com sucesso!");
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
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Tocou em doação");
            }
        });
    }
}