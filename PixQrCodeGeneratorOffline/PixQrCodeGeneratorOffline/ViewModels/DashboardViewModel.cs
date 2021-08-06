using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.ViewModels.Base;
using PixQrCodeGeneratorOffline.ViewModels.Helpers;
using PixQrCodeGeneratorOffline.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardViewModel : DashboardViewModelBase
    {
        public DashboardViewModel()
        {
            LoadDataCommand.Execute(null);

            DashboardVM = this;
        }

        public ICommand LoadDataCommand => new Command(async () => await LoadData());

        public async Task LoadData()
        {
            try
            {
                IsBusy = true;

                await Task.Delay(1000);

                await ResetProps();

                await ReloadShowInList();

                var list = _pixKeyService.GetAll();

                PixKeyList = list?.OrderBy(x => x?.FinancialInstitution?.Name).ToObservableCollection();

                PixKeyListContact = _pixKeyService.GetAll(isContact: true).ToObservableCollection();

                await LoadCurrentPixKey();
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

        private async Task ResetProps()
        {
            IsVisibleFingerPrint = Preference.FingerPrint && await CrossFingerprint.Current.IsAvailableAsync();

            ShowInList = false;
            ShowWelcome = false;
        }

        public ICommand AuthenticationCommand => new Command(async () =>
        {
            try
            {
                var request = new AuthenticationRequestConfiguration("Autenticação", "Atentique-se para continuar...");

                var result = await CrossFingerprint.Current.AuthenticateAsync(request);

                if (result.Authenticated)
                {
                    IsVisibleFingerPrint = false;
                    DialogService.Toast("Autenticado com sucesso!");
                }
                else
                {
                    DialogService.Toast("Não autenticado");
                }
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        });

        #region DashboardVMDependency

        public ICommand NavigateToAddNewKeyPageCommand => new Command(async () => await _pixKeyService.NavigateToAdd());

        //public Command<PixKey> EditKeyCommand => new Command<PixKey>(async (key) => await _pixKeyService.NavigateToEdit(key));

        //public Command<PixKey> OpenOptionsKeyCommand => new Command<PixKey>(async (key) => await _pixKeyService.NavigateToAction(key));

        #endregion

        public ICommand ChangeSelectPixKeyCommand => new Command(() => SetStatusFromCurrentPixColor());

        public ICommand SettingsCommand => new Command(async () => await NavigateModalAsync(new OptionPage()));

        public ICommand ChangeStyleListCommand => new Command(async () =>
        {
            _preferenceService.ChangeShowInList();
            await ReloadShowInList();
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

        public async Task ReloadShowInList()
        {
            try
            {
                IsBusy = true;

                await Task.Delay(500);

                ShowInList = Preference.ShowInList;

                ReloadAppColorIfShowInListStyle();

                if (ShowInList)
                {
                    CurrentIconStyleList = FontAwesomeSolid.Th;
                }

                else
                {
                    SetStatusFromCurrentPixColor();
                    CurrentIconStyleList = FontAwesomeSolid.ListAlt;
                }
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Estilo da dashboard para lista: " + ShowInList, EventType.PREFERENCE);

                IsBusy = false;
            }
        }

        public void SetStatusFromCurrentPixColor()
        {
            if (CurrentPixKey == null)
                return;

            CurrentPixKeyActions = CurrentPixKey.Actions.ToObservableCollection();

            return;

            if (ShowInList || CurrentPixKey?.FinancialInstitution?.Institution?.MaterialColor == null)
                return;

            App.LoadTheme(CurrentPixKey?.FinancialInstitution?.Institution?.MaterialColor);
        }

        #region Props

        private bool _showInList;
        public bool ShowInList
        {
            set => SetProperty(ref _showInList, value);
            get => _showInList;
        }

        private string _currentIconStyleList;
        public string CurrentIconStyleList
        {
            set => SetProperty(ref _currentIconStyleList, value);
            get => _currentIconStyleList;
        }

        private bool _isVisibleFingerPrint;
        public bool IsVisibleFingerPrint
        {
            set => SetProperty(ref _isVisibleFingerPrint, value);
            get => _isVisibleFingerPrint;
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

        private DashboardWelcome LastWelcomeItem => DashboardWelcomenList?.LastOrDefault() ?? new DashboardWelcome();

        private bool _showAddkeyOnWelcome;
        public bool ShowAddkeyOnWelcome
        {
            set => SetProperty(ref _showAddkeyOnWelcome, value);
            get => _showAddkeyOnWelcome;
        }

        #endregion



        #region Nova Dash

        private ObservableCollection<PixKeyAction> _currentPixKeyActions;
        public ObservableCollection<PixKeyAction> CurrentPixKeyActions
        {
            set => SetProperty(ref _currentPixKeyActions, value);
            get => _currentPixKeyActions;
        }

        private ObservableCollection<PixKey> _pixKeyListContact;
        public ObservableCollection<PixKey> PixKeyListContact
        {
            set => SetProperty(ref _pixKeyListContact, value);
            get => _pixKeyListContact;
        }

        #endregion

    }
}
