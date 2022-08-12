using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using Plugin.StoreReview;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class LikingViewModel : ViewModelBase
    {
        private readonly TimeSpan _secondsToToast = TimeSpan.FromSeconds(5);

        public IAsyncCommand LikeCommand => new AsyncCommand(Like);

        public IAsyncCommand UnlikeCommand => new AsyncCommand(Unlike);

        private async Task Like()
        {
            var confirm = await DialogService
                    .ConfirmAsync("Deseja deixar uma avaliação?", "Agradecemos o seu feedback!", "Sim", "Talvez depois")
                    .ConfigureAwait(false);

            await NavigateBackPopupAsync().ConfigureAwait(false);

            if (!confirm)
                return;

            await App.RequestReview().ConfigureAwait(false);
        }

        private async Task Unlike()
        {
            try
            {
                var msg = await DialogService.PromptAsync("Para você, o que precisamos melhorar?", "Obrigado pelo Feedback!", "Enviar", "Não quero informar agora");

                if (msg.Ok && !string.IsNullOrWhiteSpace(msg?.Text))
                {
                    var dic = new Dictionary<string, string>
                    {
                        { "Texto: ", msg?.Text }
                    };

                    _eventService.SendEvent("Sugestão", Services.EventType.FEEDBACK, nameof(LikingViewModel), dic);

                    DialogService.Toast("Sucesso! Mensagem enviada para os nossos desenvolvedores.", _secondsToToast);
                }

                await NavigateBackPopupAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }
    }
}
