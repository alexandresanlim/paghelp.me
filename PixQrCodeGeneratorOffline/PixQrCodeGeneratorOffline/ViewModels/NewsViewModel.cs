using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        public async Task Navigating()
        {
            try
            {
                IsBusy = true;
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Viu lista de notícias", EventType.SEE);
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

                FeedFromService = FeedFromService?.Count > 0 ? FeedFromService : await _feedService.Get("https://news.google.com/rss/search?q=pix%20-fraude%20-golpista%20-golpistas%20-erro&hl=pt-BR&gl=BR&ceid=BR%3Apt-419");

                CurrentFeedList = FeedFromService?.ToObservableCollection();

                NotFoundVisible = !(CurrentFeedList.Count > 0);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                IsBusy = false;

                foreach (var item in CurrentFeedList)
                {
                    var uri = await item.Link.GetImage();

                    if (!string.IsNullOrEmpty(uri))
                        item.Image = new UriImageSource { CachingEnabled = true, Uri = new Uri(uri) };
                }
            }
        }

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
