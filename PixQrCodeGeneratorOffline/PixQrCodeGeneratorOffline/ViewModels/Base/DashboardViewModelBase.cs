using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.ViewModels.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.ViewModels.Base
{
    public class DashboardViewModelBase : ViewModelBase
    {
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

        private ObservableCollection<DashboardWelcome> _dashboardWelcomenList;
        public ObservableCollection<DashboardWelcome> DashboardWelcomenList
        {
            set => SetProperty(ref _dashboardWelcomenList, value);
            get => _dashboardWelcomenList;
        }

        #endregion
    }
}
