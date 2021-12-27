using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardCryptoViewModel : DashboardViewModelBase
    {
        #region Commands

        public ICommand ChangeSelectCryptoKeyCommand => new Command<CryptoKey>(async (pixkey) => await ChangeSelectedCryptoKey(pixkey));

        public IAsyncCommand NavigateToAddNewKeyPageCommand => new AsyncCommand(async () => await _cryptoKeyService.NavigateToAdd());

        public ICommand ExecuteActionCommand => new Command(async () => await ExecuteAction());

        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        #endregion

        public DashboardCryptoViewModel()
        {
            LoadDataCommand.ExecuteAsync().SafeFireAndForget();

            //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            DashboardCryptoVM = this;
        }

        public async Task LoadData()
        {
            try
            {
                IsBusy = true;

                await LoadPixKey();

                //await LoadPixKeyContact();

                //await LoadBilling();

                //await LoadCurrentPixKey();

                //await CheckHasAKeyOnClipboard();

                //await LoadNews();

                //await NavigateToBenefitsPage();

                LoadHideValue();

                CurrentCryptoKeyActions = CryptoKeyAction.GetList();
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
            CryptoKeyList = _cryptoKeyService?.GetAll()?.OrderBy(x => x?.FinancialInstitution?.Name)?.ToObservableCollection() ?? new ObservableCollection<CryptoKey>();
        }

        private async Task ExecuteAction()
        {
            if (SelectedAction.Type == KeyActionType.None)
                return;

            switch (SelectedAction.Type)
            {
                //case KeyActionType.CreateBilling:
                //    CurrentCryptoKey.Command.NavigateToCreateBillingPageCommand.Execute(null);
                //    break;
                case KeyActionType.CopyKey:
                    CurrentCryptoKey.Command.CopyKeyCommand.Execute(null);
                    break;
                case KeyActionType.ShareKey:
                    CurrentCryptoKey.Command.ShareKeyCommand.Execute(null);
                    break;
                case KeyActionType.ShareOnWhatsApp:
                    CurrentCryptoKey.Command.ShareOnWhatsCommand.Execute(null);
                    break;
                //case KeyActionType.BillingList:
                //    CurrentCryptoKey.Command.NavigateToBillingCommand.Execute(null);
                //    break;
                case KeyActionType.PaymentPage:
                    CurrentCryptoKey.Command.NavigateToPaymentPageCommand.Execute(null);
                    break;
                case KeyActionType.Edit:
                    CurrentCryptoKey.Command.EditKeyCommand.Execute(null);
                    break;
                case KeyActionType.None:
                default:
                    break;
            }

            SelectedAction = new CryptoKeyAction();
        }

        #region Props

        private ObservableCollection<CryptoKey> _cryptoKeyList;
        public ObservableCollection<CryptoKey> CryptoKeyList
        {
            set => SetProperty(ref _cryptoKeyList, value);
            get => _cryptoKeyList;
        }

        private CryptoKey _currentCryptoKey;
        public CryptoKey CurrentCryptoKey
        {
            set => SetProperty(ref _currentCryptoKey, value);
            get => _currentCryptoKey;
        }

        private ObservableCollection<CryptoKeyAction> _currentCryptoKeyActions;
        public ObservableCollection<CryptoKeyAction> CurrentCryptoKeyActions
        {
            set => SetProperty(ref _currentCryptoKeyActions, value);
            get => _currentCryptoKeyActions;
        }

        private CryptoKeyAction _selectedAction;
        public CryptoKeyAction SelectedAction
        {
            set => SetProperty(ref _selectedAction, value);
            get => _selectedAction;
        }

        #endregion

        private async Task ChangeSelectedCryptoKey(CryptoKey pixkey)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CurrentCryptoKey = pixkey;
                //_statusBar.SetStatusBarColor(pixkey.FinancialInstitution.Institution.MaterialColor.PrimaryDark);
                //CurrentCryptoKeyActions = pixkey?.Actions?.ToObservableCollection() ?? new ObservableCollection<CryptoKeyAction>();
            });
        }
    }
}
