using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.DataBase;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel()
        {
            LoadDataCommand.Execute(null);
        }

        public ICommand LoadDataCommand => new Command(async () =>
        {
            try
            {
                var list = PixKeyDataBase.GetAll();

                PixKeyList = list.ToObservableCollection();

                foreach (var item in PixKeyList)
                {
                    item.RaiseCob();
                }

                await LoadCurrentPixKey();

                ReloadShowInList();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        });

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

        public ICommand NavigateToCreateBillingPageCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                await NavigateModalAsync(new CreateBillingPage(CurrentPixKey));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Navegou para criação de cobrança");

                SetIsLoading(false);
            }
        });

        public ICommand NavigateToAddNewKeyPageCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                await NavigateModalAsync(new AddPixKeyPage(this));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Navegou para adicionar nova chave");

                SetIsLoading(false);
            }
        });

        public ICommand ChangeSelectPixKeyCommand => new Command(async () =>
        {
            SetStatusFromCurrentPixColor();
        });

        public ICommand CopyKeyCommand => new Command(async () =>
        {
            await Clipboard.SetTextAsync(CurrentPixKey?.Key);
            DialogService.Toast("Chave copiada com sucesso!");
        });

        public ICommand ShareKeyCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                await ShareText(CurrentPixKey?.Key);
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Compartilhou chave");

                SetIsLoading(false);
            }
        });

        public ICommand EditKeyCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                await NavigateModalAsync(new AddPixKeyPage(this, CurrentPixKey));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Editou chave");

                SetIsLoading(false);
            }
        });

        public Command<PixKey> OpenOptionsKeyCommand => new Command<PixKey>(async (key) =>
        {
            CurrentPixKey = key;

            var options = new List<Acr.UserDialogs.ActionSheetOption>()
            {
                new Acr.UserDialogs.ActionSheetOption("Editar", () =>
                {
                    EditKeyCommand.Execute(null);
                }),
                new Acr.UserDialogs.ActionSheetOption("Copiar", () =>
                {
                    CopyKeyCommand.Execute(null);
                }),
                new Acr.UserDialogs.ActionSheetOption("Compartilhar", () =>
                {
                    ShareKeyCommand.Execute(null);
                }),
                new Acr.UserDialogs.ActionSheetOption("Criar Cobrança", () =>
                {
                    NavigateToCreateBillingPageCommand.Execute(null);
                })
            };

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = $"O que deseja fazer com a chave {CurrentPixKey.Key} ?",
                Options = options,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        });

        public ICommand SettingsCommand => new Command(async () =>
        {
            if (PixKeyList == null || PixKeyList.Count.Equals(0))
            {
                await DialogService.AlertAsync("Adicione pelo menos 1(uma) chave para customizar suas preferências.");
                return;
            }

            var options = new List<Acr.UserDialogs.ActionSheetOption>()
            {
                new Acr.UserDialogs.ActionSheetOption(PreferenceService.ShowInList ? "Exibir em carrossel" : "Exibir em lista", () =>
                {
                    PreferenceService.ShowInList = !PreferenceService.ShowInList;
                    ReloadShowInList();
                }),
            };

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "Preferências",
                Options = options,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        });

        private void ReloadShowInList()
        {
            ShowInList = PreferenceService.ShowInList;

            if (ShowInList)
            {
                var darkColor = new Style.MaterialColor
                {
                    Primary = Color.FromHex("#212121"),
                    PrimaryDark = Color.FromHex("#000000"),
                    PrimaryLight = Color.FromHex("#484848"),
                    Secondary = Color.FromHex("#34bcac"),
                    TextOnPrimary = Color.FromHex("ffffff"),
                    TextOnSecondary = Color.FromHex("000000")
                };

                App.LoadTheme(darkColor);
            }

            else
                SetStatusFromCurrentPixColor();
        }

        public void SetStatusFromCurrentPixColor()
        {
            if (ShowInList || CurrentPixKey?.Color == null)
                return;

            App.LoadTheme(CurrentPixKey?.Color);
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
    }
}
