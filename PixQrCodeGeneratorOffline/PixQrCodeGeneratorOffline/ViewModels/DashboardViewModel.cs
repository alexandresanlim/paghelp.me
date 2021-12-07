using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.ViewModels.Base;
using PixQrCodeGeneratorOffline.ViewModels.Helpers;
using PixQrCodeGeneratorOffline.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardViewModel : DashboardViewModelBase
    {
        public DashboardViewModel()
        {
            LoadDataCommand.Execute(null);

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            DashboardVM = this;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            LoadConnectionIcon();
        }

        public ICommand LoadDataCommand => new Command(async () => await LoadData());

        public async Task LoadData()
        {
            try
            {
                IsBusy = true;

                await Task.Delay(1000);

                await ResetProps();

                await LoadDashboardCustomInfo();

                await LoadPixKey();

                await LoadPixKeyContact();

                await LoadBilling();

                await LoadCurrentPixKey();

                await LoadNews();

                await NavigateToBenefitsPage();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task LoadPixKey()
        {
            try
            {
                CurrentDashboardLoadInfo.IsLoadMyKeys = true;

                await Task.Delay(500);

                PixKeyList = _pixKeyService?.GetAll()?.OrderBy(x => x?.FinancialInstitution?.Name)?.ToObservableCollection() ?? new ObservableCollection<PixKey>();

            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                CurrentDashboardLoadInfo.IsLoadMyKeys = false;
            }
        }

        public async Task LoadPixKeyContact()
        {
            try
            {
                CurrentDashboardLoadInfo.IsLoadContactKeys = true;

                await Task.Delay(500);

                PixKeyListContact = _pixKeyService?.GetAll(isContact: true)?.OrderBy(x => x?.Name)?.ToObservableCollection() ?? new ObservableCollection<PixKey>();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                CurrentDashboardLoadInfo.IsLoadContactKeys = false;
            }
        }

        public async Task LoadBilling()
        {
            try
            {
                CurrentDashboardLoadInfo.IsLoadBilling = true;

                BillingSaveList = _pixPayloadService?.GetAll()?.ToObservableCollection() ?? new ObservableCollection<PixPayload>();

                await Task.Delay(500);
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                CurrentDashboardLoadInfo.IsLoadBilling = false;
            }
        }

        public async Task LoadNews()
        {
            if (!Preference.ShowNews || Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                CurrentDashboardLoadInfo.IsLoadNews = false;
                CurrentFeedList = new ObservableCollection<Feed>();
                return;
            }

            try
            {
                CurrentDashboardLoadInfo.IsLoadNews = true;

                FeedFromService = FeedFromService?.Count > 0 ? FeedFromService : await _feedService.Get("https://news.google.com/rss/search?q=pix%20-fraude%20-golpista%20-golpistas%20-erro&hl=pt-BR&gl=BR&ceid=BR%3Apt-419");

                CurrentFeedList = FeedFromService?.ToObservableCollection();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                CurrentDashboardLoadInfo.IsLoadNews = false;

                foreach (var item in CurrentFeedList)
                {
                    var uri = await item.Link.GetImage();

                    if (!string.IsNullOrEmpty(uri))
                        item.Image = new UriImageSource { CachingEnabled = true, Uri = new System.Uri(uri) };
                }
            }
        }

        private async Task LoadDashboardCustomInfo()
        {
            CurrentDashboardCustomInfo = new DashboardCustomInfo
            {
                IsVisibleFingerPrint = Preference.FingerPrint && await CrossFingerprint.Current.IsAvailableAsync(),
                WelcomeText = DateTimeExtention.GetDashboardTitleFromPeriod(),
                WelcomeSubtitleText = DateTimeExtention.GetDashboardSubtitleFromDayOfWeed(),
            };

            LoadConnectionIcon();
        }

        private async Task NavigateToBenefitsPage()
        {
            if (PixKeyList.Count > 0)
                return;

            try
            {
                DialogService.ShowLoading();

                await Task.Delay(500);

                await NavigateAsync(new BenefitsPage());
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                DialogService.HideLoading();
            }
        }

        private void LoadConnectionIcon()
        {
            CurrentDashboardCustomInfo.ConnectionIcon = Connectivity.NetworkAccess == NetworkAccess.Internet ? FontAwesomeSolid.Wifi : FontAwesomeSolid.Plane;
        }

        private async Task ResetProps()
        {
            //ShowInList = false;
            //ShowWelcome = false;

            CurrentDashboardLoadInfo = new DashboardLoadInfo();
            //CurrentPixKey = new PixKey();
        }

        public ICommand AuthenticationCommand => new Command(async () =>
        {
            try
            {
                var request = new AuthenticationRequestConfiguration("Autenticação", "Atentique-se para continuar...");

                var result = await CrossFingerprint.Current.AuthenticateAsync(request);

                if (result.Authenticated)
                {
                    CurrentDashboardCustomInfo.IsVisibleFingerPrint = false;
                    DialogService.Toast("Autenticado com sucesso!");
                }
                else
                {
                    DialogService.Toast("Não autenticado");
                }
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        });

        #region DashboardVMDependency

        public ICommand NavigateToAddNewKeyPageCommand => new Command(async () => await _pixKeyService.NavigateToAdd());

        public ICommand NavigateToAddNewKeyPageContactCommand => new Command(async () => await _pixKeyService.NavigateToAdd(isContact: true));

        //public Command<PixKey> EditKeyCommand => new Command<PixKey>(async (key) => await _pixKeyService.NavigateToEdit(key));

        //public Command<PixKey> OpenOptionsKeyCommand => new Command<PixKey>(async (key) => await _pixKeyService.NavigateToAction(key));

        #endregion

        public ICommand ChangeSelectPixKeyCommand => new Command<PixKey>((pixkey) =>
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                CurrentPixKey = pixkey;
                CurrentPixKeyActions = pixkey?.Actions?.ToObservableCollection() ?? new ObservableCollection<PixKeyAction>();
            });

            //await Task.Run(() =>
            //{
            //    CurrentPixKey = pixkey;
            //    CurrentPixKeyActions = pixkey?.Actions?.ToObservableCollection() ?? new ObservableCollection<PixKeyAction>();
            //});
        });

        public ICommand NavigateToPreferencesCommand => new Command(async () => await NavigateAsync(new OptionPreferencePage()));

        public ICommand NavigateToGuidCommand => new Command(async () => await NavigateAsync(new GuidePage()));

        public ICommand NavigateToAboutCommand => new Command(async () => await NavigateAsync(new AboutPage()));

        public ICommand NavigateBenefitsCommand => new Command(async () => await NavigateAsync(new BenefitsPage()));

        //public ICommand ChangeStyleListCommand => new Command(async () =>
        //{
        //    _preferenceService.ChangeShowInList();
        //    await ReloadShowInList();
        //});

        public void SetStatusFromCurrentPixColor()
        {
            if (CurrentPixKey == null)
                return;

            CurrentPixKeyActions = CurrentPixKey.Actions.ToObservableCollection();

            //return;

            //if (ShowInList || CurrentPixKey?.FinancialInstitution?.Institution?.MaterialColor == null)
            //    return;

            //App.LoadTheme(CurrentPixKey?.FinancialInstitution?.Institution?.MaterialColor);
        }



        #region Nova Dash

        public ICommand ShareAllCommand => new Command(() =>
        {
            _pixKeyService.ShareAllKeys();
        });

        public ICommand RemoveAllCommand => new Command(async () =>
        {
            var success = await _pixKeyService.RemoveAll();

            if (success)
            {
                //await LoadPixKey();
                //PixKeyList.Clear();
                //CurrentPixKey = new PixKey();
                PixKeyList = new ObservableCollection<PixKey>();

                //await LoadCurrentPixKey(null);
            }
        });

        public ICommand RemoveAllKeyContactCommand => new Command(async () =>
        {
            var success = await _pixKeyService.RemoveAll(isContact: true);

            if (success)
            {
                PixKeyListContact = new ObservableCollection<PixKey>();
                //await LoadPixKeyContact();
            }
        });

        public ICommand RemoveAllBillingCommand => new Command(async () =>
        {
            var success = await _pixPayloadService.RemoveAll();

            if (success)
            {
                await LoadBilling();
            }

        });

        private ObservableCollection<PixKeyAction> _currentPixKeyActions;
        public ObservableCollection<PixKeyAction> CurrentPixKeyActions
        {
            set => SetProperty(ref _currentPixKeyActions, value);
            get => _currentPixKeyActions;
        }

        private ObservableCollection<PixKey> _pixKeyListContact;
        public ObservableCollection<PixKey> PixKeyListContact
        {
            set => SetProperty(ref _pixKeyListContact, value);
            get => _pixKeyListContact;
        }

        private ObservableCollection<PixPayload> _billingSaveList;
        public ObservableCollection<PixPayload> BillingSaveList
        {
            set => SetProperty(ref _billingSaveList, value);
            get => _billingSaveList;
        }

        public List<Feed> FeedFromService { get; set; }

        private ObservableCollection<Feed> _currentFeedList;
        public ObservableCollection<Feed> CurrentFeedList
        {
            get => _currentFeedList;
            set => SetProperty(ref _currentFeedList, value);
        }

        private DashboardLoadInfo _currentDashboardLoadInfo;
        public DashboardLoadInfo CurrentDashboardLoadInfo
        {
            get => _currentDashboardLoadInfo;
            set => SetProperty(ref _currentDashboardLoadInfo, value);
        }

        private DashboardCustomInfo _currentDashboardCustomInfo;
        public DashboardCustomInfo CurrentDashboardCustomInfo
        {
            get => _currentDashboardCustomInfo;
            set => SetProperty(ref _currentDashboardCustomInfo, value);
        }

        #endregion

    }

    public class DashboardLoadInfo : Models.Base.NotifyObjectBase
    {
        private bool _isLoadMyKeys = true;
        public bool IsLoadMyKeys
        {
            get => _isLoadMyKeys;
            set => SetProperty(ref _isLoadMyKeys, value);
        }

        private bool _isLoadContactKeys = true;
        public bool IsLoadContactKeys
        {
            get => _isLoadContactKeys;
            set => SetProperty(ref _isLoadContactKeys, value);
        }

        private bool _isLoadBilling = true;
        public bool IsLoadBilling
        {
            get => _isLoadBilling;
            set => SetProperty(ref _isLoadBilling, value);
        }

        private bool _isLoadNews = true;
        public bool IsLoadNews
        {
            get => _isLoadNews;
            set => SetProperty(ref _isLoadNews, value);
        }
    }

    public class DashboardCustomInfo : Models.Base.NotifyObjectBase
    {
        private bool _isVisibleFingerPrint = true;
        public bool IsVisibleFingerPrint
        {
            set => SetProperty(ref _isVisibleFingerPrint, value);
            get => _isVisibleFingerPrint;
        }

        private bool _showWelcome = true;
        public bool ShowWelcome
        {
            set => SetProperty(ref _showWelcome, value);
            get => _showWelcome;
        }

        private string _connectionIcon;
        public string ConnectionIcon
        {
            set => SetProperty(ref _connectionIcon, value);
            get => _connectionIcon;
        }

        private string _themeIcon;
        public string ThemeIcon
        {
            set => SetProperty(ref _themeIcon, value);
            get => _themeIcon;
        }

        private string _welcomeText;
        public string WelcomeText
        {
            set => SetProperty(ref _welcomeText, value);
            get => _welcomeText;
        }

        private string _welcomeSubtitleText;
        public string WelcomeSubtitleText
        {
            set => SetProperty(ref _welcomeSubtitleText, value);
            get => _welcomeSubtitleText;
        }
    }
}
