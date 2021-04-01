using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.DataBase;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
                ResetProps();

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

        private void ResetProps()
        {
            WelcomeText =
                "🔐 Seguro: Guarde suas chaves localmente de maneira criptografada e sem conexão com a internet. \n\n" +
                "🔀 Prático: Compartilhe suas chaves rapidamente.\n\n" +
                "🤙 Customizável: Exiba em formato de carrossel ou lista, com suporte a dark mode.\n\n" +
                "🤑 Cobranças: Gere Qr Codes para pagamento.\n\n" +
                "💾 Backup: Local e automático.\n\n" +
                "Mais novidades vindo aí!";
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
                new Acr.UserDialogs.ActionSheetOption("Campartilhar todas as chaves", () =>
                {
                    ShareAllKeys();
                }),
            };

            if (PixKeyList.Count > 1)
            {
                options.Add(new Acr.UserDialogs.ActionSheetOption($"Excluir todas as {PixKeyList.Count} chaves", async () =>
                {
                    await RemoveAllKeys();
                }));
            }

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

        public void ShareAllKeys()
        {
            var info = "";

            foreach (var item in PixKeyList)
            {
                info += item.InstitutionAndKey + "\n";
            }

            if (string.IsNullOrWhiteSpace(info))
                return;

            try
            {
                var options = new List<Acr.UserDialogs.ActionSheetOption>()
                {
                    new Acr.UserDialogs.ActionSheetOption("Compartilhar", async () =>
                    {
                        await Share.RequestAsync(new ShareTextRequest
                        {
                            Text = info,
                            Title = "Compartilhar Texto"
                        });
                    }),
                    new Acr.UserDialogs.ActionSheetOption("Salvar em txt e compartilhar", async () =>
                    {
                       var path = string.Empty;

                        path = Path.Combine(FileSystem.CacheDirectory, "ChavesPix.txt");

                        File.WriteAllText(path, info);

                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = "Compartilhar Arquivo",
                            File = new ShareFile(path)
                        });
                    }),
                };

                DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
                {
                    Title = "Selecione uma opção:",
                    Options = options,
                    Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                    {
                        return;
                    })
                });
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Compartilhou todas as chaves");
            }
        }

        private void ReloadShowInList()
        {
            ShowInList = PreferenceService.ShowInList;

            if (ShowInList)
                ReloadAppColorIfShowInListStyle();

            else
                SetStatusFromCurrentPixColor();
        }

        private async Task RemoveAllKeys()
        {
            var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir todas as " + PixKeyList.Count + " chaves?", "Confirmação", "Sim, tenho certeza", "Cancelar");

            if (!confirm)
                return;

            try
            {
                var success = PixKeyDataBase.RemoveAll();

                if (success)
                {
                    PixKeyList.Clear();

                    await LoadCurrentPixKey(null);

                    DialogService.Toast("Todas as chaves foram removidas com sucesso!");

                    NavigateBack();
                }

                else
                    DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app");
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Removeu todas as chaves");
            }
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

        private string _welcomeText;
        public string WelcomeText
        {
            set => SetProperty(ref _welcomeText, value);
            get => _welcomeText;
        }
    }
}
