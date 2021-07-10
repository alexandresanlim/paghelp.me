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
                var list = _pixKeyService?.GetAll(isContact: true);

                PixKeyList = list?.OrderBy(x => x?.Name)?.ToObservableCollection() ?? new ObservableCollection<PixKey>();

                await LoadCurrentPixKey();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        }

        public ICommand NavigateToAddNewKeyPageCommand => new Command(async () => await _pixKeyService.NavigateToAdd(isContact: true));
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
