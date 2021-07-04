using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.ViewModels.Base;
using PixQrCodeGeneratorOffline.Views;
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
    public class DashboardContactViewModel : DashboardViewModelBase
    {

        public DashboardContactViewModel()
        {
            LoadDataCommand.Execute(null);

            DashboardContactVM = this;
        }

        public ICommand LoadDataCommand => new Command(async () => await LoadData());

        public ICommand SettingsCommand => new Command(async () => await NavigateModalAsync(new OptionContactPage()));

        public async Task LoadData()
        {
            try
            {
                //await ResetProps();

                //await ReloadShowInList();

                var list = _pixKeyService?.GetAll()?.Where(x => x.IsContact);

                PixKeyList = list?.OrderBy(x => x?.FinancialInstitution?.Name)?.ToObservableCollection() ?? new ObservableCollection<PixKey>();

                await LoadCurrentPixKey();

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

        #region DashboardVMDependency

        public ICommand NavigateToAddNewKeyPageCommand => new Command(async () => await _pixKeyService.NavigateToAdd(isContact: true));

        public Command<PixKey> EditKeyCommand => new Command<PixKey>(async (key) => await _pixKeyService.NavigateToEdit(key, isContact: true));

        //public Command<PixKey> OpenOptionsKeyCommand => new Command<PixKey>(async (key) => await _pixKeyService.NavigateToAction(key));

        #endregion

        //private ObservableCollection<PixKey> _pixKeyList;
        //public ObservableCollection<PixKey> PixKeyList
        //{
        //    set => SetProperty(ref _pixKeyList, value);
        //    get => _pixKeyList;
        //}

        //private PixKey _currentPixKey;
        //public PixKey CurrentPixKey
        //{
        //    set => SetProperty(ref _currentPixKey, value);
        //    get => _currentPixKey;
        //}
    }
}
