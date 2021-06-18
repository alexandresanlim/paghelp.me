using PixQrCodeGeneratorOffline.Controls;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public async Task Navigating()
        {
            try
            {
                IsBusy = true;

                //await Task.Run(async () =>
                //{
                //    SetIsLoading(true);

                //    await Task.Delay(500);

                //    await LoadData();
                //});
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Viu lista de notícias", EventType.SEE);

                //SetIsLoading(false);
            }
        }

        public ICommand LoadDataCommand => new Command(async () =>
        {
            await LoadData();
        });

        public List<Feed> FeedFromService { get; set; }

        public async Task LoadData()
        {
            try
            {
                IsBusy = true;

                FeedFromService = FeedFromService?.Count > 0 ? FeedFromService : await FeedService.Get("https://news.google.com/rss/search?q=pix%20-fraude%20-golpista%20-golpistas%20-erro%20-golpe%20-hack%20-hacker&hl=pt-BR&gl=BR&ceid=BR%3Apt-419");

                CurrentFeedList = FeedFromService?.Where(x => x.PublishDate != null)?.OrderByDescending(x => x.PublishDate)?.ToObservableCollection();

                NotFoundVisible = !(CurrentFeedList.Count > 0);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ICommand ItemTappedCommand => new Command<Feed>(async (feed) =>
        {
            if (feed.Validation.IsValid)
            {
                DialogService.Toast("Link para notícia não encontrado.");
                return;
            }

            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                await NavigateModalAsync(new WebViewPage(feed.Link, feed?.Title));
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                 _eventService.SendEvent("Viu uma notícia", EventType.SEE, new Dictionary<string, string> { { "Título: ", feed?.Title } });

                SetIsLoading(false);
            }
        });

        public ICommand ShareCommand => new Command<Feed>(async (feed) =>
        {
            if (feed.Validation.IsValid)
            {
                DialogService.Toast("Link para notícia não encontrado.");
                return;
            }

            try
            {

                SetIsLoading(true);

                await Task.Delay(500);

                await _externalActionService.ShareText(feed.Link.AbsoluteUri);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                 _eventService.SendEvent("Compartilhou uma notícia: " + feed?.Title, EventType.SHARE, new Dictionary<string, string> { { "Título: ", feed?.Title } });

                SetIsLoading(false);
            }
        });

        private ObservableCollection<Feed> _currentFeedList;
        public ObservableCollection<Feed> CurrentFeedList
        {
            get => _currentFeedList;
            set => SetProperty(ref _currentFeedList, value);
        }

        private bool _notFoundVisible;
        public bool NotFoundVisible
        {
            get => _notFoundVisible;
            set => SetProperty(ref _notFoundVisible, value);
        }
    }
}
