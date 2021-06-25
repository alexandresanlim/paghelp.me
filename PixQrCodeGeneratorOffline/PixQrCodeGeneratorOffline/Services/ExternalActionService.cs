using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

            await Share.RequestAsync(new Xamarin.Essentials.ShareTextRequest
            {
                Text = text,
                Title = "Escolha uma opção"
            });
        }

        public async Task CopyText(string text, string textSuccess = "Copiado com sucesso!")
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                DialogService.Toast("Texto a ser copiado é inválido");
                return;
            }

            await Clipboard.SetTextAsync(text);
            DialogService.Toast(textSuccess);
        }

        public string GenerateTxtFile(string contents, string fileName)
        {
            if (string.IsNullOrWhiteSpace(contents) || string.IsNullOrWhiteSpace(fileName))
            {
                DialogService.Toast("Não foi possível garar o arquivo");
                return "";
            }

            var path = Path.Combine(FileSystem.CacheDirectory, fileName + ".txt");

            File.WriteAllText(path, contents);

            return path;
        }

        public async Task ShareFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                DialogService.Toast("O caminho para o arquivo é inválido");
                return;
            }

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Compartilhar Arquivo",
                File = new ShareFile(path)
            });
        }
    }
}
