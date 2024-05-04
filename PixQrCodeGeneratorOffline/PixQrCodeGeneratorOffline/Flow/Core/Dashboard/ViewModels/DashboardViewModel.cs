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

        public IAsyncCommand NavigateToNewsCommand => new AsyncCommand(async () => await NavigateAsync(new NewsPage()));

        public IAsyncCommand NavigateToContactsCommand => new AsyncCommand(async () => await NavigateAsync(new DashboardContactPage()));

        public IAsyncCommand NavigateToAddNewKeyPageCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToAdd().ConfigureAwait(false));

        public IAsyncCommand ExecuteActionCommand => new AsyncCommand(ExecuteAction);

        public ICommand ChangeSelectPixKeyCommand => new Command<PixKey>(ChangeSelectedPixKey);

        public IAsyncCommand ShareAllCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToShareAllKeys(PixKeyList));

        public IAsyncCommand RemoveAllCommand => new AsyncCommand(RemoveAllKeys);

        public IAsyncCommand RemoveAllBillingCommand => new AsyncCommand(RemoveAllBilling);

        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        public IAsyncCommand ExportToFileCommand => new AsyncCommand(() => _pixKeyService.ExportToFile(PixKeyList));

        public IAsyncCommand ExportToFileBillingCommand => new AsyncCommand(() => _pixPayloadService.ExportToFile(BillingSaveList));

        public IAsyncCommand NavigateToFGTSAdCommand => new AsyncCommand(NavigateToFGTSAd);

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

                    LoadContactKeys();

                    LoadBilling();

                    LoadCurrentPixKey();

                    await CheckHasAKeyOnClipboard().ConfigureAwait(false);

                    CurrentPixKeyActions = PixKeyAction.GetList();

                    if (!Preference.LikingAppMsgWasShowed && (PixKeyList?.Count > 0) && Preference.AreYouLikingAppMsgCount >= Constants.COUNTER_TO_SHOWED_LIKING_PAGE)
                    {
                        _preferenceService.ChangeLikingAppMsgWasShowed(true);
                        await WaitAndExecute(3000, async () => await NavigateToLikingPage().ConfigureAwait(false));
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

        public void LoadPixKey()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                PixKeyList = _pixKeyService?.GetAll().ToObservableCollection() ?? new ObservableCollection<PixKey>();
            });
        }

        public void LoadContactKeys()
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

        private void ChangeSelectedPixKey(PixKey pixkey) => MainThread.BeginInvokeOnMainThread(() =>
        {
            CurrentPixKey = pixkey;
            _feedbackService.Feedback();
        });

        private Task ExecuteAction()
        {
            if (SelectedAction?.Type == KeyActionType.None)
                return Task.CompletedTask;

            var commandToReturn = SelectedAction.Type switch
            {
                KeyActionType.CreateBilling => CurrentPixKey.Command.NavigateToCreateBillingPageCommand.ExecuteAsync(),
                KeyActionType.CreateBillingDynamic => CurrentPixKey.Command.NavigateToCreateBillingDynamicPageCommand.ExecuteAsync(),
                KeyActionType.CopyKey => CurrentPixKey.Command.CopyKeyCommand.ExecuteAsync(),
                KeyActionType.ShareKey => CurrentPixKey.Command.ShareKeyCommand.ExecuteAsync(),
                KeyActionType.ShareOnWhatsApp => CurrentPixKey.Command.ShareOnWhatsCommand.ExecuteAsync(),
                KeyActionType.BillingList => CurrentPixKey.Command.NavigateToBillingCommand.ExecuteAsync(),
                KeyActionType.PaymentPage => CurrentPixKey.Command.NavigateToPaymentPageCommand.ExecuteAsync(),
                KeyActionType.Edit => CurrentPixKey.Command.EditKeyCommand.ExecuteAsync(),
                KeyActionType.Delete => DeleteCurrentKeyAsync(),
                KeyActionType.DownloadQRCode => CurrentPixKey.Payload.Commands.DownloadQrCodeCommand.ExecuteAsync(),
                _ => Task.CompletedTask,
            };

            SelectedAction = new PixKeyAction();

            return commandToReturn;
        }

        private async Task DeleteCurrentKeyAsync()
        {
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
        }

        private async Task CheckHasAKeyOnClipboard()
        {
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync().ConfigureAwait(false);

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

        //private void LoadConnectionIcon()
        //{
        //    CurrentDashboardCustomInfo.ConnectionIcon = Connectivity.NetworkAccess == NetworkAccess.Internet ? FontAwesomeSolid.Wifi : FontAwesomeSolid.Plane;
        //}

        private async Task RemoveAllKeys()
        {
            var success = await _pixKeyService.RemoveAll().ConfigureAwait(false);

            if (success)
                PixKeyList = new ObservableCollection<PixKey>();
        }
    
        private async Task RemoveAllBilling()
        {
            var success = await _pixPayloadService.RemoveAll().ConfigureAwait(false);

            if (success)
            {
                LoadBilling();
            }
        }

        private async Task NavigateToFGTSAd()
        {
            try
            {
                await _externalActionService.ShareOnWhats("Olá, vim através do Paghelp.me! e gostaria de informações para antecipar o meu saque de aniversário do FGTS.", "+5518996822936");
            }
            catch (Exception ex)
            {
                ex.SendToLog();
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

        public IList<Feed> FeedFromService { get; set; }

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
