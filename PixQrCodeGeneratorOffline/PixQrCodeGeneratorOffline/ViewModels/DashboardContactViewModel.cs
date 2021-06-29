using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardContactViewModel : BaseViewModel
    {

        public DashboardContactViewModel()
        {
            LoadDataCommand.Execute(null);
        }

        public ICommand LoadDataCommand => new Command(async () => await LoadData());

        public async Task LoadData()
        {
            try
            {
                //await ResetProps();

                //await ReloadShowInList();

                var list = _pixKeyService.GetAll();

                PixKeyList = list?.OrderBy(x => x?.FinancialInstitution?.Name).ToObservableCollection();

                //await LoadCurrentPixKey();

                //if (!(PixKeyList.Count > 0))
                //    DashboardWelcomenList = DashboardWelcome.GetList();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
            }
        }

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
    }
}
