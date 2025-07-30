using AsyncAwaitBestPractices.MVVM;
using pix_dynamic_payload_generator.net;
using pix_dynamic_payload_generator.net.Models.Enums;
using pix_dynamic_payload_generator.net.Models.Psps;
using pix_dynamic_payload_generator.net.Requests.RequestModels;
using pix_dynamic_payload_generator.net.Requests.RequestServices;
using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Services;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using PspHomologated = pix_dynamic_payload_generator.net.Models.Psps.Homologated;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class CreateBillingViewModel : ViewModelBase
    {
        public string AddDescriptionValue => "Adicionar Descrição";

        public bool isDynamic = false;

        private void ResetCurrentValue()
        {
            ValueInput = "";
            CurrentDescription = AddDescriptionValue;
            CurrentCob = new PixCob();
            SetValueCurrencyFormat();
        }

        public Command<PixKey> LoadDataCommand => new Command<PixKey>((pixKey) =>
        {
            try
            {
                CurrentPixKey = pixKey;

                ResetCurrentValue();

                LoadStyle();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        });

        private void LoadStyle()
        {
            CurrentStyleFromKey = CurrentPixKey?.FinancialInstitution?.Institution?.MaterialColor;
        }

        public Command<string> InputTextCommand => new Command<string>((text) =>
        {
            try
            {
                _feedbackService.Feedback();

                if (string.IsNullOrEmpty(text))
                    ValueInput = ValueInput.RemoveLastChar();

                else
                {
                    if (ValueInput.Length < 7)
                        ValueInput += text;
                }

                SetValueCurrencyFormat();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        });

        public ICommand ResetCurrentValueCommand => new Command(() =>
        {
            ResetCurrentValue();
        });

        private void SetValueCurrencyFormat()
        {
            string valueFromString = Regex.Replace(ValueInput, @"\D", "");

            decimal d;

            if (valueFromString.Length <= 0)
                d = 0m;

            long valueLong;
            if (!long.TryParse(valueFromString, out valueLong))
                d = 0m;

            if (valueLong <= 0)
                d = 0m;

            d = valueLong / 100m;

            var finalString = Convert.ToDecimal(d, new System.Globalization.CultureInfo("en-US")).ToString("N");

            CurrentCob.Value = finalString;
        }

        public string ValueInput { get; set; }

        public IAsyncCommand NavigateToPaymentPageCommand => new AsyncCommand(async () =>
        {

            CurrentCob.IsDynamic = isDynamic;

            if (CurrentCob.IsDynamic && (CurrentCob.Value == null || !(decimal.Parse(CurrentCob.Value) > 0)))
            {
                DialogService.Toast("Não é possível criar cobranças dinâmicas com valor igual a zero", TimeSpan.FromSeconds(5));
                return;
            }

            try
            {
                SetIsLoading(true);

                var pixPaylod = _pixPayloadService.Create(CurrentPixKey, CurrentCob);

                if (!_pixPayloadService.IsValid(pixPaylod))
                    return;

                await pixPaylod.Commands.NavigateToPaymentPageCommand.ExecuteAsync();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetIsLoading(false);
            }
        });

        public ICommand SetDescriptionCommand => new Command(() =>
        {
            try
            {
                var promptConfig = new Acr.UserDialogs.PromptConfig
                {
                    CancelText = "Cancelar",
                    OkText = "Ok",
                    Title = "Descrição",
                    Message = "Digite o texto que aparecerá para o pagador",
                    Placeholder = "Ex: Pedido 1 (Max 76 caracteres)",
                    Text = !CurrentDescription.Equals(AddDescriptionValue) ? CurrentDescription : "",
                    MaxLength = 76,
                    OnAction = new Action<Acr.UserDialogs.PromptResult>((result) =>
                    {
                        if (!result.Ok)
                            return;

                        var text = result?.Text;

                        if (string.IsNullOrEmpty(text))
                        {
                            CurrentDescription = AddDescriptionValue;
                            CurrentCob.Description = "";
                            return;
                        }

                        CurrentDescription = text;
                        CurrentCob.Description = text;
                    })
                };

                DialogService.Prompt(promptConfig);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        });

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }

        private PixCob _currentCob;
        public PixCob CurrentCob
        {
            set => SetProperty(ref _currentCob, value);
            get => _currentCob;
        }

        private string _currentDescription;
        public string CurrentDescription
        {
            set => SetProperty(ref _currentDescription, value);
            get => _currentDescription;
        }
    }
}
