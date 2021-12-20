using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardCryptoViewModel : ViewModelBase
    {
        #region Commands

        public ICommand ChangeSelectPixKeyCommand => new Command<CryptoKey>(async (pixkey) => await ChangeSelectedPixKey(pixkey));

        public IAsyncCommand NavigateToAddNewKeyPageCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToAdd());

        #endregion

        #region Props

        private ObservableCollection<CryptoKey> _pixKeyList;
        public ObservableCollection<CryptoKey> PixKeyList
        {
            set => SetProperty(ref _pixKeyList, value);
            get => _pixKeyList;
        }

        private CryptoKey _currentPixKey;
        public CryptoKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }

        private ObservableCollection<CryptoKeyAction> _currentPixKeyActions;
        public ObservableCollection<CryptoKeyAction> CurrentPixKeyActions
        {
            set => SetProperty(ref _currentPixKeyActions, value);
            get => _currentPixKeyActions;
        }

        #endregion

        private async Task ChangeSelectedPixKey(CryptoKey pixkey)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CurrentPixKey = pixkey;
                //_statusBar.SetStatusBarColor(pixkey.FinancialInstitution.Institution.MaterialColor.PrimaryDark);
                //CurrentPixKeyActions = pixkey?.Actions?.ToObservableCollection() ?? new ObservableCollection<PixKeyAction>();
            });
        }
    }
}
