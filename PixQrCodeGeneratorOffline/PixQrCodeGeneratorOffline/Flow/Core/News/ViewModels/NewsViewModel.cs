using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        public void Navigating()
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

        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        public IList<Feed> FeedFromService { get; set; }

        public async Task LoadData()
        {
            try
            {
                IsBusy = true;

                FeedFromService ??= await _feedService.Get("https://news.google.com/rss/search?q=pix%20-fraude%20-golpista%20-golpistas%20-erro%20-bolsonaro%20-lula&hl=pt-BR&gl=BR&ceid=BR%3Apt-419");

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
