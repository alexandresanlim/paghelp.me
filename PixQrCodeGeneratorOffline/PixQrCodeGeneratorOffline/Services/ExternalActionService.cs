using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Services
{
    public class ExternalActionService : IExternalActionService
    {
        protected IUserDialogs DialogService => UserDialogs.Instance;

        public async Task ShareText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                DialogService.Toast("Texto a ser compartilhado é inválido");
                return;
            }

            await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareTextRequest
            {
                Text = text,
                Title = "Escolha uma opção"
            });
        }

        public async Task CopyText(string text, string textSuccess = "Copiado com sucesso!")
        {
            await Xamarin.Essentials.Clipboard.SetTextAsync(text);
            DialogService.Toast(textSuccess);
        }
    }
}
