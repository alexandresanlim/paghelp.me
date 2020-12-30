using PixQrCodeGeneratorOffline.Controls;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public ICommand LoadDataCommand => new Command(async () =>
        {
            await LoadData();
        });

        public async Task Navigating()
        {
            try
            {
                await Task.Run(async () =>
                {
                    SetIsLoading(true);

                    await Task.Delay(1000);

                    await LoadData();
                });
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                SetIsLoading(false);
            }
        }

        public async Task LoadData()
        {
            try
            {
                IsBusy = true;

                var itens = FeedService.Get("https://news.google.com/rss/search?q=pix&hl=pt-BR&gl=BR&ceid=BR%3Apt-419");

                CurrentFeedList = itens?.ToObservableCollection();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ICommand ItemTappedCommand => new Command<Feed>(async (item) =>
        {
            await Xamarin.Essentials.Browser.OpenAsync(item.Link, new CustomBrowserLaunchOptions());
        });

        public ICommand ShareCommand => new Command<Feed>(async (item) =>
        {
            await ShareText(item.Link.AbsoluteUri);
        });

        private ObservableCollection<Feed> _currentFeedList;
        public ObservableCollection<Feed> CurrentFeedList
        {
            get => _currentFeedList;
            set => SetProperty(ref _currentFeedList, value);
        }
    }
}
