using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardContactViewModel : DashboardViewModelBase
    {
        #region Commands

        public ICommand LoadDataCommand => new Command(LoadData);

        public IAsyncCommand NavigateToAddNewKeyPageCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToAdd(isContact: true));

        #endregion

        public DashboardContactViewModel()
        {
            LoadDataCommand.Execute(null);

            DashboardContactVM = this;
        }

        public void LoadData()
        {
            try
            {
                var list = _pixKeyService?.GetAll(isContact: true);

                PixKeyList = list?.OrderBy(x => x?.Name)?.ToObservableCollection() ?? new ObservableCollection<PixKey>();

                LoadCurrentPixKey();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        }
    }

    //public static class DashboardContactViewModelExtention
    //{
    //    private static async Task LoadDataContact(this DashboardContactViewModel dashboardViewModelBase)
    //    {
    //        //var list = _pixKeyService?.GetAll(isContact: true);

    //        //dashboardViewModelBase.PixKeyList = list?.OrderBy(x => x?.FinancialInstitution?.Name)?.ToObservableCollection() ?? new ObservableCollection<PixKey>();

    //        //await dashboardViewModelBase.LoadCurrentPixKey();
    //    }
    //}
}
