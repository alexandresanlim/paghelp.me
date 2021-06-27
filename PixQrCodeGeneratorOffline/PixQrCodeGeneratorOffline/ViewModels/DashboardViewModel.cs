using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
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
    public class DashboardViewModel : BaseViewModel
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
                await ResetProps();

                await ReloadShowInList();

                var list = _pixKeyService.GetAll();

                PixKeyList = list?.OrderBy(x => x?.FinancialInstitution?.Name).ToObservableCollection();

                await LoadCurrentPixKey();

                if (!(PixKeyList.Count > 0))
                    await LoadDashboardWelcome();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
            }
        }

        private async Task ResetProps()
        {
            IsVisibleFingerPrint = Preference.FingerPrint && await CrossFingerprint.Current.IsAvailableAsync();

            ShowInList = false;
            ShowInCarousel = false;
            ShowWelcome = false;
        }

        public async Task LoadCurrentPixKey(PixKey pixKeySelected = null)
        {
            if (PixKeyList == null || !(PixKeyList.Count > 0))
                ShowWelcome = true;

            else
            {
                CurrentPixKey = pixKeySelected ?? PixKeyList.FirstOrDefault();
                ShowWelcome = false;
            }
        }

        private async Task LoadDashboardWelcome()
        {
            DashboardWelcomenList = new ObservableCollection<DashboardWelcome>
            {
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.Lock,
                    Title = "Seguro",
                    Description = "Guarde suas chaves localmente de maneira criptografada e sem conexão com a internet, com suporte a autenticação biométrica se disponível pelo seu aparelho.",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.HandHoldingUsd,
                    Title = "Cobranças",
                    Description = "Gere Qr Codes para pagamento.",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.ThumbsUp,
                    Title = "Prático",
                    Description = "Compartilhe uma única ou todas suas chaves rapidamente, incluindo com geração de txt",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.Cogs,
                    Title = "Customizável",
                    Description = "Exiba em formato de carrossel ou lista, com suporte a dark e light mode,",
                    Unconnection = true
                },

                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.Save,
                    Title = "Backup",
                    Description = "Local, automático e criptografado.",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.ExclamationTriangle,
                    Title = "IMPORTANTE!",
                    Description = "- Para sua segurança, não fazemos conexão direta com o seu banco, sendo assim não será possível ver saldo ou realizar transferências, para isso use o app oficial do mesmo e jamais forneça esse tipo de acesso para terceiros. \n\n - Não temos quaisquer relação com o governo federal do Brasil, porém seguimos a risca, todos manuais e recomendações de padronização e segurança disponibilizados pela instituição."
                }
            };
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

        public ICommand EditKeyCommand => new Command(async () => await _pixKeyService.NavigateToEdit(CurrentPixKey));

        public Command<PixKey> OpenOptionsKeyCommand => new Command<PixKey>(async (key) => await _pixKeyService.NavigateToAction(key));

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
                SetIsLoading(true, "Aguarde...");

                await Task.Delay(500);

                ShowInList = Preference.ShowInList;
                ShowInCarousel = !ShowInList;

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

                SetIsLoading(false);
            }
        }

        public void SetStatusFromCurrentPixColor()
        {
            if (ShowInList || CurrentPixKey?.FinancialInstitution?.Institution?.MaterialColor == null)
                return;

            App.LoadTheme(CurrentPixKey?.FinancialInstitution?.Institution?.MaterialColor);
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

        private bool _showWelcome;
        public bool ShowWelcome
        {
            set => SetProperty(ref _showWelcome, value);
            get => _showWelcome;
        }

        private bool _showInList;
        public bool ShowInList
        {
            set => SetProperty(ref _showInList, value);
            get => _showInList;
        }

        private bool _showInCarousel;
        public bool ShowInCarousel
        {
            set => SetProperty(ref _showInCarousel, value);
            get => _showInCarousel;
        }

        private string _welcomeText;
        public string WelcomeText
        {
            set => SetProperty(ref _welcomeText, value);
            get => _welcomeText;
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

    public class DashboardWelcome
    {
        public string Emoji { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Unconnection { get; set; }
    }
}
