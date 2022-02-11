using Acr.UserDialogs;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.ViewModels.Base;
using PixQrCodeGeneratorOffline.Views;
using System;
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
        #region Commands

        public IAsyncCommand NavigateToGuidCommand => new AsyncCommand(async () => await NavigateAsync(new GuidePage()));

        public IAsyncCommand NavigateToAddNewKeyPageCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToAdd());

        public IAsyncCommand NavigateToAddNewKeyPageContactCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToAdd(isContact: true));

        public ICommand ExecuteActionCommand => new Command(ExecuteAction);

        public ICommand ChangeSelectPixKeyCommand => new Command<PixKey>(ChangeSelectedPixKey);

        public IAsyncCommand ShareAllCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToShareAllKeys(PixKeyList));

        public IAsyncCommand RemoveAllCommand => new AsyncCommand(RemoveAllKeys);

        public IAsyncCommand RemoveAllKeyContactCommand => new AsyncCommand(RemoveAllContactKeys);

        public IAsyncCommand RemoveAllBillingCommand => new AsyncCommand(RemoveAllBilling);

        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        #endregion

        public DashboardViewModel()
        {
            LoadDataCommand.ExecuteAsync().SafeFireAndForget();

            //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            DashboardVM = this;
        }

        //private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        //{
        //    LoadConnectionIcon();
        //}

        public async Task LoadData()
        {
            try
            {
                IsBusy = true;

                ResetProps();

                LoadPixKey();

                LoadPixKeyContact();

                LoadBilling();

                LoadCurrentPixKey();

                await CheckHasAKeyOnClipboard();

                //await LoadNews();

                await NavigateToBenefitsPage();

                //LoadHideValue();

                CurrentPixKeyActions = PixKeyAction.GetList();
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

        public void LoadPixKey() => PixKeyList = _pixKeyService?.GetAll()?.OrderBy(x => x?.FinancialInstitution?.Name)?.ToObservableCollection() ?? new ObservableCollection<PixKey>();

        public void LoadPixKeyContact() => PixKeyListContact = _pixKeyService?.GetAll(isContact: true)?.OrderBy(x => x?.Name)?.ToObservableCollection() ?? new ObservableCollection<PixKey>();

        public void LoadBilling() => BillingSaveList = _pixPayloadService?.GetAll()?.ToObservableCollection() ?? new ObservableCollection<PixPayload>();

        private void ResetProps()
        {
            CurrentDashboardLoadInfo = new DashboardLoadInfo();
            CurrentDashboardCustomInfo = new DashboardCustomInfo();
        }

        private void ChangeSelectedPixKey(PixKey pixkey)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CurrentPixKey = pixkey;
                //_statusBar.SetStatusBarColor(pixkey.FinancialInstitution.Institution.MaterialColor.PrimaryDark);
                //CurrentPixKeyActions = pixkey?.Actions?.ToObservableCollection() ?? new ObservableCollection<PixKeyAction>();
            });
        }

        private void ExecuteAction()
        {
            if (SelectedAction.Type == KeyActionType.None)
                return;

            switch (SelectedAction.Type)
            {
                case KeyActionType.CreateBilling:
                    CurrentPixKey.Command.NavigateToCreateBillingPageCommand.Execute(null);
                    break;
                case KeyActionType.CopyKey:
                    CurrentPixKey.Command.CopyKeyCommand.Execute(null);
                    break;
                case KeyActionType.ShareKey:
                    CurrentPixKey.Command.ShareKeyCommand.Execute(null);
                    break;
                case KeyActionType.ShareOnWhatsApp:
                    CurrentPixKey.Command.ShareOnWhatsCommand.Execute(null);
                    break;
                case KeyActionType.BillingList:
                    CurrentPixKey.Command.NavigateToBillingCommand.Execute(null);
                    break;
                case KeyActionType.PaymentPage:
                    CurrentPixKey.Command.NavigateToPaymentPageCommand.Execute(null);
                    break;
                case KeyActionType.Edit:
                    CurrentPixKey.Command.EditKeyCommand.Execute(null);
                    break;
                case KeyActionType.None:
                default:
                    break;
            }

            SelectedAction = new PixKeyAction();
        }

        private async Task CheckHasAKeyOnClipboard()
        {
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();

                if (text.IsAKey())
                {
                    var hasKey = _pixKeyService.GetAll(x => x.Key.ToLower().Equals(text.ToLower()))?.FirstOrDefault();

                    if (hasKey != null && hasKey?.Id > 0)
                        return;

                    var confirm = await DialogService.ConfirmAsync($"{text}, deseja adiciona-la agora?", "Tem uma chave na sua àrea de tranferência", "Sim", "Cancelar");

                    if (!confirm)
                        return;

                    var options = new List<ActionSheetOption>()
                    {
                        new ActionSheetOption("Minha", async () =>
                        {
                            await NavigateAsync(new AddPixKeyPage());
                        }),
                        new ActionSheetOption("De um contato", async () =>
                        {
                            await NavigateAsync(new AddPixKeyPage(isContact: true));
                        }),
                    };

                    DialogService.ActionSheet(new ActionSheetConfig
                    {
                        Title = "Essa chave é:",
                        Options = options,
                        Cancel = new ActionSheetOption("Cancelar", () =>
                        {
                            return;
                        }),
                    });
                }
            }
        }

        [Obsolete("Google desativou a funcionalidade de feed")]
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
            catch (Exception e)
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
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                DialogService.HideLoading();
            }
        }

        //private void LoadConnectionIcon()
        //{
        //    CurrentDashboardCustomInfo.ConnectionIcon = Connectivity.NetworkAccess == NetworkAccess.Internet ? FontAwesomeSolid.Wifi : FontAwesomeSolid.Plane;
        //}

        private async Task RemoveAllKeys()
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
        }

        private async Task RemoveAllContactKeys()
        {
            var success = await _pixKeyService.RemoveAll(isContact: true);

            if (success)
            {
                PixKeyListContact = new ObservableCollection<PixKey>();
                //await LoadPixKeyContact();
            }
        }

        private async Task RemoveAllBilling()
        {
            var success = await _pixPayloadService.RemoveAll();

            if (success)
            {
                LoadBilling();
            }
        }

        #region Props

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

        private PixKeyAction _selectedAction;
        public PixKeyAction SelectedAction
        {
            set => SetProperty(ref _selectedAction, value);
            get => _selectedAction;
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
        private string _connectionIcon;
        public string ConnectionIcon
        {
            set => SetProperty(ref _connectionIcon, value);
            get => _connectionIcon;
        }

        //private string _welcomeText;
        //public string WelcomeText
        //{
        //    set => SetProperty(ref _welcomeText, value);
        //    get => _welcomeText;
        //}

        //private string _welcomeSubtitleText;
        //public string WelcomeSubtitleText
        //{
        //    set => SetProperty(ref _welcomeSubtitleText, value);
        //    get => _welcomeSubtitleText;
        //}
    }
}
