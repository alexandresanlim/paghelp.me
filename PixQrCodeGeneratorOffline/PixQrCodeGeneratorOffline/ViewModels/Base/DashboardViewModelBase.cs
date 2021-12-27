using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels.Base
{
    public class DashboardViewModelBase : ViewModelBase
    {
        public ICommand HideValueCommand => new Command(HideValue);

        public async Task LoadCurrentPixKey(PixKey pixKeySelected = null)
        {
            CurrentPixKey = pixKeySelected ?? PixKeyList.FirstOrDefault();

            //if (PixKeyList == null || !(PixKeyList.Count > 0))
            //{
            //    DashboardWelcomenList = DashboardWelcome.GetList();
            //    ShowWelcome = true;
            //}

            //else
            //{
            //    CurrentPixKey = pixKeySelected ?? PixKeyList.FirstOrDefault();
            //    ShowWelcome = false;
            //}
        }

        private void HideValue()
        {
            Preference.HideData = !Preference.HideData;
            LoadHideValue();
        }

        public void LoadHideValue()
        {
            IsHideValue = Preference.HideData;
        }

        #region props

        private ObservableCollection<PixKey> _pixKeyList;
        public ObservableCollection<PixKey> PixKeyList
        {
            set => SetProperty(ref _pixKeyList, value);
            get => _pixKeyList;
        }

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }

        private bool _isHideValue;
        public bool IsHideValue
        {
            get => _isHideValue;
            set => SetProperty(ref _isHideValue, value);
        }

        #endregion
    }
}
