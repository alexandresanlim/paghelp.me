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
        }

        public ICommand LoadDataCommand => new Command(async () => await LoadData());

        public async Task LoadData()
        {
            try
            {
                await ResetProps();

                await ReloadShowInList();

                var list = _pixKeyService.GetAll();

                PixKeyList = list.ToObservableCollection();

                await LoadCurrentPixKey();


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

            WelcomeText =
                "🔐 Seguro: Guarde suas chaves localmente de maneira criptografada e sem conexão com a internet, com suporte a autenticação biométrica se suportado. \n\n" +
                "🔀 Prático: Compartilhe suas chaves rapidamente.\n\n" +
                "🤙 Customizável: Exiba em formato de carrossel ou lista, com suporte a dark e light mode.\n\n" +
                "🤑 Cobranças: Gere Qr Codes para pagamento.\n\n" +
                "💾 Backup: Local e automático.\n\n" +
                "E mais! \n\n" +
                "⚠ IMPORTANTE! Não fazemos conexão direta com o seu banco, sendo assim não será possível ver saldo ou realizar transferências, para isso use o app do seu banco.";
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

        #region DashboardDependency

        public ICommand NavigateToAddNewKeyPageCommand => new Command(async () => await _pixKeyService.NavigateToAdd(this));

        public ICommand EditKeyCommand => new Command(async () => await _pixKeyService.NavigateToEdit(this, CurrentPixKey));

        public Command<PixKey> OpenOptionsKeyCommand => new Command<PixKey>(async (key) => await NavigateAsync(new PixKeyActionPage(this, key)));

        #endregion

        public ICommand ChangeSelectPixKeyCommand => new Command(() => SetStatusFromCurrentPixColor());

        public ICommand SettingsCommand => new Command(async () => await NavigateAsync(new OptionPage()));

        public ICommand ChangeStyleListCommand => new Command(async () =>
        {
            _preferenceService.ChangeShowInList();
            await ReloadShowInList();
        });

        public async Task ReloadShowInList()
        {
            try
            {
                SetIsLoading(true, "Aguarde...");

                await Task.Delay(500);

                ShowInList = Preference.ShowInList;
                ShowInCarousel = !ShowInList;

                if (ShowInList)
                {
                    ReloadAppColorIfShowInListStyle();
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

        private Payload _currentPayload;
        public Payload CurrentPayload
        {
            set => SetProperty(ref _currentPayload, value);
            get => _currentPayload;
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
    }
}
