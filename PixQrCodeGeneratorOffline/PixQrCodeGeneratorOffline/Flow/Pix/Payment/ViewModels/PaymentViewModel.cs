using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using pix_dynamic_payload_generator.net.Models;
using pix_dynamic_payload_generator.net.Requests.RequestServices;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        private Cob currentCob;

        public bool paymentCanceled;

        public CancellationTokenSource waitingPaymentTokenSource = new CancellationTokenSource();

        public PaymentViewModel(PayloadBase payload)
        {
            paymentCanceled = true;

            LoadData(payload).SafeFireAndForget((ex) => ex.SendToLog());
        }

        #region Commands

        public IAsyncCommand SaveCommand => new AsyncCommand(Save);

        public ICommand ChangeIsActionVisibleCommand => new Command(ChangeIsActionVisible);

        #endregion

        private async Task LoadData(PayloadBase payloadParameter)
        {
            try
            {
                SetIsLoading();

                CurrentPaylodBase = payloadParameter;
                CurrentInfo = new PaymentViewModelInfo();

                if (payloadParameter is PixPayload pixPayload)
                {
                    CurrentPixPaylod = pixPayload;
                    CurrentInfo.Color = pixPayload.PixKey?.FinancialInstitution?.Institution?.MaterialColor;
                    CurrentInfo.Value = pixPayload.PixCob?.Viewer?.ValuePresentation;
                    CurrentInfo.Name = pixPayload.PixKey?.Viewer?.NamePresentation;
                    CurrentInfo.Institution = pixPayload?.PixKey?.Viewer?.InstitutionPresentation;
                    CurrentInfo.Key = $"| Chave: {pixPayload?.PixKey?.Viewer?.KeyPresentation}";

                    if (pixPayload.PixCob != null && pixPayload.PixCob.IsDynamic)
                    {
                        await WatingPayment(waitingPaymentTokenSource.Token).ConfigureAwait(false);
                    }
                    else
                    {
                        SaveButtonIsVisible = !(CurrentPixPaylod?.Id > 0) && CurrentPixPaylod.PixCob.HasValue();
                    }
                }

                else if (payloadParameter is CryptoPayload cryptoPayload)
                {
                    CurrentCryptoPaylod = cryptoPayload;
                    CurrentInfo.Color = cryptoPayload?.CryptoKey?.FinancialInstitution?.Institution?.MaterialColor;
                    CurrentInfo.Institution = $"Criptomoeda: {cryptoPayload?.CryptoKey?.Viewer?.InstitutionPresentation}";
                    CurrentInfo.Key = $"| Chave: {cryptoPayload?.CryptoKey?.Viewer?.KeyPresentation}";
                }

                IsActionVisible = false;

                LoadHelpPhrase();
            }
            catch (OperationCanceledException)
            {
                paymentCanceled = true;
            }
            catch (Exception ex)
            {
                ex.SendToLog();
            }
            finally
            {
                SetIsLoading(false);
            }
        }

        private void LoadHelpPhrase()
        {
            var paymentType = (CurrentPaylodBase?.Type ?? PayloadType.Pix) == PayloadType.Crypto ? "Cripto" : "Pix";

            CurrentInfo.HelpPhrase =
                $"O pagador precisa abrir o app que vai fazer a transferência {paymentType} e escanear este QR Code ou colar o código copia e cola.";
        }

        private async Task Save()
        {
            var identity = await DialogService.PromptAsync(new Acr.UserDialogs.PromptConfig
            {
                Title = "Identificador",
                Message = "Digite um texto para identificar esta cobrança",
                CancelText = "Cancelar",
                OkText = "Salvar"
            });

            if (!identity.Ok)
                return;

            if (string.IsNullOrEmpty(identity?.Text))
            {
                DialogService.Toast("Ops! É preciso digitar um identificador para salvar");
                return;
            }

            CurrentPixPaylod.Identity = identity.Text;

            var success = _pixPayloadService.Save(CurrentPixPaylod);

            if (success)
            {
                DashboardVM.LoadBilling();
            }

            else
            {
                DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app.");
            }
        }

        private async Task WatingPayment(CancellationToken token)
        {
            paymentCanceled = false;

            var cobRequest = new CobRequestService();

            await WaitAndExecute(10000, async () =>
            {
                var isPaid = false;

                do
                {
                    try
                    {
                        token.ThrowIfCancellationRequested();

                        if (!paymentCanceled)
                        {
                            await WaitAndExecute(3000, async () =>
                            {
                                currentCob = await cobRequest.GetByTxId(currentCob.Txid).ConfigureAwait(false);
                                isPaid = currentCob.HasPix && currentCob.StatusPagamento == PaymenStatus.PAGO_TOTALMENTE;
                            }, token);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        paymentCanceled = true;
                    }
                    catch (Exception ex)
                    {
                        paymentCanceled = true;
                        ex.SendToLog();
                    }

                }
                while (!paymentCanceled && !isPaid);

                if (isPaid)
                    DialogService.Toast("Pago com sucesso!", TimeSpan.FromSeconds(5));

                if (PopupNavigation.Instance.PopupStack.Any())
                    await NavigateBackPopupAsync().ConfigureAwait(false);

            }, token).ConfigureAwait(false);
        }

        private void ChangeIsActionVisible()
        {
            IsActionVisible = !IsActionVisible;
        }

        private PixPayload _currentPixPaylod;
        public PixPayload CurrentPixPaylod
        {
            set => SetProperty(ref _currentPixPaylod, value);
            get => _currentPixPaylod;
        }

        private CryptoPayload _currentCryptoPaylod;
        public CryptoPayload CurrentCryptoPaylod
        {
            set => SetProperty(ref _currentCryptoPaylod, value);
            get => _currentCryptoPaylod;
        }

        private PayloadBase _currentPaylodBase;
        public PayloadBase CurrentPaylodBase
        {
            set => SetProperty(ref _currentPaylodBase, value);
            get => _currentPaylodBase;
        }

        private bool _isActionVisible;
        public bool IsActionVisible
        {
            set => SetProperty(ref _isActionVisible, value);
            get => _isActionVisible;
        }

        private bool _saveButtonIsVisible;
        public bool SaveButtonIsVisible
        {
            set => SetProperty(ref _saveButtonIsVisible, value);
            get => _saveButtonIsVisible;
        }

        private PaymentViewModelInfo _currentInfo;
        public PaymentViewModelInfo CurrentInfo
        {
            set => SetProperty(ref _currentInfo, value);
            get => _currentInfo;
        }
    }

    public class PaymentViewModelInfo : NotifyObjectBase
    {
        private string _helpPhrase;
        public string HelpPhrase
        {
            set => SetProperty(ref _helpPhrase, value);
            get => _helpPhrase;
        }

        private string _value;
        public string Value
        {
            set => SetProperty(ref _value, value);
            get => _value;
        }

        private string _name;
        public string Name
        {
            set => SetProperty(ref _name, value);
            get => _name;
        }

        private string _institution;
        public string Institution
        {
            set => SetProperty(ref _institution, value);
            get => _institution;
        }

        private string _key;
        public string Key
        {
            set => SetProperty(ref _key, value);
            get => _key;
        }

        private MaterialColor _color;
        public MaterialColor Color
        {
            set => SetProperty(ref _color, value);
            get => _color;
        }

        private bool _isPaid;
        public bool IsPaid
        {
            set => SetProperty(ref _isPaid, value);
            get => _isPaid;
        }
    }
}
