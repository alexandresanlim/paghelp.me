using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands
{
    public class FeedCommand : CommandBase, IFeedCommand
    {
        public ICommand NavigateToWebViewCommand { get; private set; }

        public ICommand ShareCommand { get; private set; }

        public FeedCommand Create(Feed feed)
        {
            return feed.IsValid() ? new FeedCommand
            {
                NavigateToWebViewCommand = GetNavigateToWebViewCommand(feed),
                ShareCommand = GetShareCommand(feed),
            } : new FeedCommand();
        }

        private Command GetNavigateToWebViewCommand(Feed feed)
        {
            return new Command(async () =>
            {
                if (!feed.IsValid())
                {
                    DialogService.Toast("Link para notícia não encontrado.");
                    return;
                }

                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await Shell.Current.Navigation.PushAsync(new WebViewPage(feed.Link, feed?.Title));
                }
                catch (Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Viu uma notícia", EventType.SEE, nameof(FeedCommand), new Dictionary<string, string> { { "Título: ", feed?.Title } });

                    DialogService.HideLoading();
                }
            });
        }

        private Command GetShareCommand(Feed feed)
        {
            return new Command(async () =>
            {
                if (!feed.IsValid())
                {
                    DialogService.Toast("Link para notícia não encontrado.");
                    return;
                }

                try
                {

                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await _externalActionService.ShareText(feed.Link.AbsoluteUri);
                }
                catch (Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Compartilhou uma notícia", EventType.SHARE, nameof(FeedCommand), new Dictionary<string, string> { { "Título: ", feed?.Title } });

                    DialogService.HideLoading();
                }
            });
        }
    }
}
