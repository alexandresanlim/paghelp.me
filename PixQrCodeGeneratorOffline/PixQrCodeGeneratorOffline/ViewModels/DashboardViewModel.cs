using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
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
        private readonly IPixKeyService _pixKeyService;
        private readonly IPixPayloadService _pixPayloadService;

        public DashboardViewModel()
        {
            _pixKeyService = DependencyService.Get<IPixKeyService>();
            _pixPayloadService = DependencyService.Get<IPixPayloadService>();

            LoadDataCommand.Execute(null);
        }

        public ICommand LoadDataCommand => new Command(async () =>
        {
            try
            {
                await ResetProps();

                var list = _pixKeyService.GetAll();

                PixKeyList = list.ToObservableCollection();

                await LoadCurrentPixKey();

                await ReloadShowInList();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
        });

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

        private async Task ResetProps()
        {
            IsVisibleFingerPrint = PreferenceService.FingerPrint && await CrossFingerprint.Current.IsAvailableAsync();

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

        public ICommand NavigateToPaymentPageCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                var pixPaylod = _pixPayloadService.Create(CurrentPixKey);

                await NavigateModalAsync(new PaymentPage(pixPaylod));
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Navegou para pagina de pagamento a partir da dashboard");

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
            var options = new List<Acr.UserDialogs.ActionSheetOption>
            {
                new Acr.UserDialogs.ActionSheetOption("Preferências", async () =>
                {
                   await OptionsPreferenceOpen();
                }),

                new Acr.UserDialogs.ActionSheetOption("Chaves", async () =>
                {
                    await OptionsKeysOpen();
                })
            };

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "Opções",
                Options = options,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        });

        private async Task OptionsPreferenceOpen()
        {
            var preferences = new List<Acr.UserDialogs.ActionSheetOption>();

            if (PixKeyList != null && PixKeyList.Count > 0)
            {
                preferences.Add(new Acr.UserDialogs.ActionSheetOption(PreferenceService.ShowInList ? "Exibir em carrossel" : "Exibir em lista", async () =>
                 {
                     PreferenceService.ShowInList = !PreferenceService.ShowInList;
                     await ReloadShowInList();
                 }));
            }

            if (await CrossFingerprint.Current.IsAvailableAsync())
            {
                preferences.Add(new Acr.UserDialogs.ActionSheetOption((PreferenceService.FingerPrint ? "Remover" : "Adicionar") + " autenticação biométrica", async () =>
                {
                    await SetFingerPrint();
                }));
            }

            if (preferences.Count.Equals(0))
            {
                DialogService.Toast("Nenhum preferência disponível para o seu dispositivo");
                return;
            }

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "Preferências",
                Options = preferences,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        }

        private async Task OptionsKeysOpen()
        {
            if (PixKeyList == null || PixKeyList.Count.Equals(0))
            {
                await DialogService.AlertAsync("Adicione pelo menos 1(uma) chave para ver opções.");
                return;
            }

            var keys = new List<Acr.UserDialogs.ActionSheetOption>
            {
                new Acr.UserDialogs.ActionSheetOption("Campartilhar todas as chaves", () =>
                {
                    ShareAllKeys();
                })
            };

            if (PixKeyList.Count > 1)
            {
                keys.Add(new Acr.UserDialogs.ActionSheetOption($"Excluir todas as {PixKeyList.Count} chaves", async () =>
                {
                    await RemoveAllKeys();
                }));
            }

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "Chaves",
                Options = keys,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        }

        public void ShareAllKeys()
        {
            var info = "";

            foreach (var item in PixKeyList)
            {
                info += item.Viewer.InstitutionAndKey + "\n";
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

        private async Task ReloadShowInList()
        {
            try
            {
                SetIsLoading(true, "Aguarde...");

                await Task.Delay(500);

                ShowInList = PreferenceService.ShowInList;

                if (ShowInList)
                    ReloadAppColorIfShowInListStyle();

                else
                    SetStatusFromCurrentPixColor();
            }
            catch (System.Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Trocou a dashboard para mostrar em lista: " + ShowInList);

                SetIsLoading(false);
            }
        }

        private async Task RemoveAllKeys()
        {
            var confirm = await DialogService.ConfirmAsync("Tem certeza que deseja excluir todas as " + PixKeyList.Count + " chaves?", "Confirmação", "Sim, tenho certeza", "Cancelar");

            if (!confirm)
                return;

            try
            {
                var success = _pixKeyService.RemoveAll();

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

        private async Task SetFingerPrint()
        {
            var confirmMsg = "Tem certeza que deseja " + (PreferenceService.FingerPrint ? "remover" : "adicionar") + " autenticação biométrica? Na próxima vez que você entrar, " + (PreferenceService.FingerPrint ? "não será" : "será") + " necessário se autenticar para realizar quaisquer ações.";

            if (!await DialogService.ConfirmAsync(confirmMsg, "Confirmação", "Confirmar", "Cancelar"))
                return;

            PreferenceService.FingerPrint = !PreferenceService.FingerPrint;

            DialogService.Toast("Preferência de entrada, salva com sucesso!");
        }

        public void SetStatusFromCurrentPixColor()
        {
            if (ShowInList || CurrentPixKey?.FinancialInstitution?.Institution?.Color == null)
                return;

            App.LoadTheme(CurrentPixKey?.FinancialInstitution?.Institution?.Color);
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

        private bool _isVisibleFingerPrint;
        public bool IsVisibleFingerPrint
        {
            set => SetProperty(ref _isVisibleFingerPrint, value);
            get => _isVisibleFingerPrint;
        }
    }
}
