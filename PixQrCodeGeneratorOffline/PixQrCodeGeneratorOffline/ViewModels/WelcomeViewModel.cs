using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.ViewModels.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        public WelcomeViewModel()
        {
            LoadData.Execute(null);
        }

        ICommand LoadData => new Command(() =>
        {
            DashboardWelcomenList = DashboardWelcome.GetList();
        });

        public ICommand NavigateToAddNewKeyPageCommand => new Command(async () =>
        {
            await _pixKeyService.NavigateToAdd();
            //NavigateBack();
        });

        public ICommand WelcomeNextCommand => new Command(async () =>
        {
            try
            {
                if (ShowAddkeyOnWelcome)
                    return;

                ActualWelcomeNextPosition = ActualWelcomeNextPosition++;
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        });

        public ICommand SkipWelcomeCommand => new Command(async () =>
        {
            try
            {
                CurrentDashboardWelcome = LastWelcomeItem;
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        });

        public ICommand CurrentWelcomeItemChangedCommand => new Command(() => CheckIsLastItemOnWelcome());

        private void CheckIsLastItemOnWelcome()
        {
            ShowAddkeyOnWelcome = CurrentDashboardWelcome == LastWelcomeItem;
        }

        private int _actualWelcomeNextPosition;
        public int ActualWelcomeNextPosition
        {
            set => SetProperty(ref _actualWelcomeNextPosition, value);
            get => _actualWelcomeNextPosition;
        }

        private DashboardWelcome _currentDashboardWelcome;
        public DashboardWelcome CurrentDashboardWelcome
        {
            set => SetProperty(ref _currentDashboardWelcome, value);
            get => _currentDashboardWelcome;
        }

        private ObservableCollection<DashboardWelcome> _dashboardWelcomenList;
        public ObservableCollection<DashboardWelcome> DashboardWelcomenList
        {
            set => SetProperty(ref _dashboardWelcomenList, value);
            get => _dashboardWelcomenList;
        }

        private DashboardWelcome LastWelcomeItem => DashboardWelcomenList?.LastOrDefault() ?? new DashboardWelcome();

        private bool _showAddkeyOnWelcome;
        public bool ShowAddkeyOnWelcome
        {
            set => SetProperty(ref _showAddkeyOnWelcome, value);
            get => _showAddkeyOnWelcome;
        }
    }
}
