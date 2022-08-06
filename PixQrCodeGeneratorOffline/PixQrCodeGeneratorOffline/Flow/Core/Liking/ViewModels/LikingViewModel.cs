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
            try
            {
                await CrossStoreReview.Current.RequestReview(false).ConfigureAwait(false);

                DialogService.Toast("Agradecemos o seu feedback! Nossa missão é melhorar-mos cada vez mais.", _secondsToToast);

                NavigateBack();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        private async Task Unlike()
        {
            try
            {
                var msg = await DialogService.PromptAsync("Para você, em que precisamos melhorar?", "Que pena :/", "Enviar", "Não quero informar agora");

                if (msg.Ok && !string.IsNullOrWhiteSpace(msg?.Text))
                {
                    var dic = new Dictionary<string, string>
                {
                    { "Texto: ", msg?.Text }
                };

                    _eventService.SendEvent("Sugestão", Services.EventType.FEEDBACK, nameof(LikingViewModel), dic);

                    await CrossStoreReview.Current.RequestReview(false);

                    DialogService.Toast("Agradecemos o seu feedback! Mensagem enviada para os desenvolvedores.", _secondsToToast);
                }

                NavigateBack();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }
    }
}
