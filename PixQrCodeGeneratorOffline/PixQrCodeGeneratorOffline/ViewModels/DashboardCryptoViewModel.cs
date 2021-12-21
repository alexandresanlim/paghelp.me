using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardCryptoViewModel : ViewModelBase
    {
        #region Commands

        public ICommand ChangeSelectCryptoKeyCommand => new Command<CryptoKey>(async (pixkey) => await ChangeSelectedCryptoKey(pixkey));

        public IAsyncCommand NavigateToAddNewKeyPageCommand => new AsyncCommand(async () => await _cryptoKeyService.NavigateToAdd());

        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        #endregion

        public DashboardCryptoViewModel()
        {
            LoadDataCommand.ExecuteAsync().SafeFireAndForget();

            //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            //DashboardVM = this;
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

        private ObservableCollection<CryptoKeyAction> _currentPixKeyActions;
        public ObservableCollection<CryptoKeyAction> CurrentPixKeyActions
        {
            set => SetProperty(ref _currentPixKeyActions, value);
            get => _currentPixKeyActions;
        }

        #endregion

        private async Task ChangeSelectedCryptoKey(CryptoKey pixkey)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CurrentCryptoKey = pixkey;
                //_statusBar.SetStatusBarColor(pixkey.FinancialInstitution.Institution.MaterialColor.PrimaryDark);
                CurrentPixKeyActions = pixkey?.Actions?.ToObservableCollection() ?? new ObservableCollection<CryptoKeyAction>();
            });
        }
    }
}
