using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.ViewModels.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class BenefitsViewModel : ViewModelBase
    {
        public BenefitsViewModel()
        {
            LoadDataCommand.Execute(null);
        }

        public ICommand LoadDataCommand => new Command(LoadShowWelcome);

        public IAsyncCommand NavigateToAddNewKeyPageCommand => new AsyncCommand(async () => await _pixKeyService.NavigateToAdd());

        private void LoadShowWelcome()
        {
            DashboardWelcomenList = DashboardWelcome.GetList();
        }

        private ObservableCollection<DashboardWelcome> _dashboardWelcomenList;
        public ObservableCollection<DashboardWelcome> DashboardWelcomenList
        {
            set => SetProperty(ref _dashboardWelcomenList, value);
            get => _dashboardWelcomenList;
        } 
    }
}
