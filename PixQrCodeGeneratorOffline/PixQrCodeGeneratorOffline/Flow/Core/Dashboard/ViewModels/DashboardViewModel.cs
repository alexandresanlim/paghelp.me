using Acr.UserDialogs;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Helpers;
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

        public IAsyncCommand ExecuteActionCommand => new AsyncCommand(ExecuteAction);

        public ICommand ChangeSelectPixKeyCommand => new Command<PixKey>(ChangeSelectedPixKey);

        public IAsyncCommand ShareAllCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToShareAllKeys(PixKeyList));

        public IAsyncCommand RemoveAllCommand => new AsyncCommand(RemoveAllKeys);

        public IAsyncCommand RemoveAllKeyContactCommand => new AsyncCommand(RemoveAllContactKeys);

        public IAsyncCommand RemoveAllBillingCommand => new AsyncCommand(RemoveAllBilling);

        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        public ICommand DeleteContactKeyCommand => new AsyncCommand<PixKey>(DeleteContactKey);

        public IAsyncCommand ExportToFileCommand => new AsyncCommand(() => _pixKeyService.ExportToFile(PixKeyList));

        #endregion

        public DashboardViewModel()
        {
            LoadDataCommand.ExecuteAsync().SafeFireAndForget();

            DashboardVM = this;
        }

        private async Task LoadData()
        {
            await LoadAuthenticationPage(async () =>
            {
                try
                {
                    IsBusy = true;

                    LoadPixKey();

                    LoadPixKeyContact();

                    LoadBilling();

                    LoadCurrentPixKey();

                    await CheckHasAKeyOnClipboard().ConfigureAwait(false);

                    CurrentPixKeyActions = PixKeyAction.GetList();

                    if (!Preference.LikingAppMsgWasShowed && (PixKeyList?.Count > 0 || PixKeyListContact?.Count > 0) && Preference.AreYouLikingAppMsgCount >= Constants.COUNTER_TO_SHOWED_LIKING_PAGE)
                    {
                        _preferenceService.ChangeLikingAppMsgWasShowed(true);
                        await WaitAndExecute(5000, async () => await NavigateToLikingPage());
                    }
                }
                catch (Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }

        public void LoadPixKey() => PixKeyList = _pixKeyService?.GetAll().ToObservableCollection() ?? new ObservableCollection<PixKey>();

        public void LoadPixKeyContact()
        {
            PixKeyListContact = new ObservableCollection<PixKey>();

            PixKeyListContact = _pixKeyService?
            .GetAll(isContact: true)?.OrderBy(x => x?.Name)?
            .ToObservableCollection() ?? new ObservableCollection<PixKey>();
        }

        public void LoadBilling()
        {
            BillingSaveList = new ObservableCollection<PixPayload>();

            BillingSaveList = _pixPayloadService?
            .GetAll()?.ToObservableCollection() ?? new ObservableCollection<PixPayload>();
        }

        private void ChangeSelectedPixKey(PixKey pixkey) => MainThread.BeginInvokeOnMainThread(() => CurrentPixKey = pixkey);

        private async Task ExecuteAction()
        {
            if (SelectedAction?.Type == KeyActionType.None)
                return;

            switch (SelectedAction.Type)
            {
                case KeyActionType.CreateBilling:
                    await CurrentPixKey.Command.NavigateToCreateBillingPageCommand.ExecuteAsync();
                    break;
                case KeyActionType.CopyKey:
                    await CurrentPixKey.Command.CopyKeyCommand.ExecuteAsync();
                    break;
                case KeyActionType.ShareKey:
                    await CurrentPixKey.Command.ShareKeyCommand.ExecuteAsync();
                    break;
                case KeyActionType.ShareOnWhatsApp:
                    await CurrentPixKey.Command.ShareOnWhatsCommand.ExecuteAsync();
                    break;
                case KeyActionType.BillingList:
                    await CurrentPixKey.Command.NavigateToBillingCommand.ExecuteAsync();
                    break;
                case KeyActionType.PaymentPage:
                    await CurrentPixKey.Command.NavigateToPaymentPageCommand.ExecuteAsync();
                    break;
                case KeyActionType.Edit:
                    await CurrentPixKey.Command.EditKeyCommand.ExecuteAsync();
                    break;
                case KeyActionType.Delete:
                    var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir a chave " + CurrentPixKey.Key + "?", "Confirmação", "Sim", "Cancelar");

                    if (!confirm)
                        return;

                    try
                    {
                        SetIsLoading(true);

                        var success = _pixKeyService.Remove(CurrentPixKey);

                        if (success)
                        {
                            int index = PixKeyList.IndexOf(PixKeyList.FirstOrDefault(x => x.Id == CurrentPixKey.Id));

                            if (index != -1)
                            {
                                PixKeyList.RemoveAt(index);
                                CurrentPixKey = PixKeyList?.FirstOrDefault() ?? new PixKey();
                            }

                            if (PixKeyList.Count == 0)
                            {
                                PixKeyList = new ObservableCollection<PixKey>();
                            }

                            DialogService.Toast("Chave removida com sucesso");
                        }

                        else
                        {
                            DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");
                        }
                    }
                    catch (Exception e)
                    {
                        e.SendToLog();
                    }
                    finally
                    {
                        SetIsLoading(false);
                    }
                    break;
                case KeyActionType.DownloadQRCode:
                    await CurrentPixKey.Payload.Commands.DownloadQrCodeCommand.ExecuteAsync();
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
                CurrentFeedList = new ObservableCollection<Feed>();
                return;
            }

            try
            {
                FeedFromService = FeedFromService?.Count > 0 ? FeedFromService : await _feedService.Get("https://news.google.com/rss/search?q=pix%20-fraude%20-golpista%20-golpistas%20-erro&hl=pt-BR&gl=BR&ceid=BR%3Apt-419");

                CurrentFeedList = FeedFromService?.ToObservableCollection();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                foreach (var item in CurrentFeedList)
                {
                    var uri = await item.Link.GetImage();

                    if (!string.IsNullOrEmpty(uri))
                        item.Image = new UriImageSource { CachingEnabled = true, Uri = new System.Uri(uri) };
                }
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
                PixKeyList = new ObservableCollection<PixKey>();
        }

        #region Contact

        private async Task RemoveAllContactKeys()
        {
            var success = await _pixKeyService.RemoveAll(isContact: true);

            if (success)
                PixKeyListContact = new ObservableCollection<PixKey>();
        }

        private async Task DeleteContactKey(PixKey contactKey)
        {
            var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir a chave do contato " + contactKey?.Name + "?", "Confirmação", "Sim", "Cancelar");

            if (!confirm)
                return;

            try
            {
                SetIsLoading(true);

                var success = _pixKeyService.Remove(contactKey);

                if (success)
                {
                    int index = PixKeyListContact.IndexOf(PixKeyListContact.FirstOrDefault(x => x.Id == contactKey.Id));

                    if (index != -1)
                        PixKeyListContact.RemoveAt(index);

                    if (PixKeyListContact.Count == 0)
                    {
                        PixKeyListContact = new ObservableCollection<PixKey>();
                    }

                    DialogService.Toast("Chave removida com sucesso");
                }

                else
                    ShowToastErrorMessage();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetIsLoading(false);
            }
        }

        #endregion

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

        private PixKeyAction _selectedAction;
        public PixKeyAction SelectedAction
        {
            set => SetProperty(ref _selectedAction, value);
            get => _selectedAction;
        }

        #endregion

    }
}
