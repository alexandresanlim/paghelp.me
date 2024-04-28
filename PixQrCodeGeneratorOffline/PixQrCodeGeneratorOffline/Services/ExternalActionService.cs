using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Models.DataStatic.Files.Base;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Services
{
    public class ExternalActionService : IExternalActionService
    {
        protected IUserDialogs DialogService => UserDialogs.Instance;

        protected readonly IFeedbackService _feedbackService;

        public ExternalActionService()
        {
            _feedbackService = DependencyService.Get<IFeedbackService>();
        }

        public async Task ShareText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                DialogService.Toast("Texto a ser compartilhado é inválido");
                return;
            }

            _feedbackService.Feedback();

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Escolha uma opção"
            });
        }

        public async Task ShareOnWhats(string text, string phoneNumber = null)
        {
            var textAndPhone = "send?text=" + Uri.EscapeDataString(text);

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                phoneNumber = phoneNumber.Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                textAndPhone += "&phone=" + phoneNumber;
            }

            var supportsUri = await Launcher.CanOpenAsync("whatsapp://");

            _feedbackService.Feedback();

            if (supportsUri)
                await Launcher.OpenAsync(new Uri("whatsapp://" + textAndPhone));

            else
                await Launcher.OpenAsync(new Uri("https://api.whatsapp.com/" + textAndPhone));
        }

        public async Task CopyText(string text, string textSuccess = "Copiado com sucesso!", Color? backgroundToast = null, Color? foregroundToast = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                DialogService.Toast("Texto inválido");
                return;
            }

            await Clipboard.SetTextAsync(text);

            _feedbackService.Feedback();

            DialogService.Toast(new ToastConfig(textSuccess)
            {
                BackgroundColor = backgroundToast ?? App.ThemeColors.PrimaryDark,
                MessageTextColor = foregroundToast ?? App.ThemeColors.TextOnPrimary,
            });
        }

        public string BuildPathFile(string contents, string fileName, IFileExtension extension)
        {
            if (string.IsNullOrWhiteSpace(contents) || string.IsNullOrWhiteSpace(fileName) || extension == null)
            {
                DialogService.Toast("Não foi possível garar o arquivo");
                return "";
            }

            var path = Path.Combine(FileSystem.CacheDirectory, fileName + extension.SetOnFileName);

            File.WriteAllText(path, contents);

            return path;
        }

        public async Task ShareFile(string path, IFileExtension extension)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                DialogService.Toast("O caminho para o arquivo é inválido");
                return;
            }

            _feedbackService.Feedback();

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Compartilhar Arquivo",
                File = new ShareFile(path, extension.ContentType)
            });
        }
    }
}
