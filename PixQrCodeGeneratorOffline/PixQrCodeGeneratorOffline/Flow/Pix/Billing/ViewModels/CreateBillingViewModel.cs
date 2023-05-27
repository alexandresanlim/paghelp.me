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

            //return;

            //if (CurrentStyleFromKey != null)
            //    App.LoadTheme(CurrentStyleFromKey);
        }

        public Command<string> InputTextCommand => new Command<string>((text) =>
        {
            try
            {
                try { Xamarin.Essentials.HapticFeedback.Perform(Xamarin.Essentials.HapticFeedbackType.Click); } catch (Exception) { }

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

                if (CurrentCob.IsDynamic)
                {
                    var pspClientId = new PspClientId(
                        _productionClientId: "Client_Id_505c65a6e2cd5e048d583b80f8da2356a7230275",
                        _homologationClientId: "Client_Id_51d92e9836716a4ab9b3ec1d9d34f6644ac28d69");

                    var pspClientSecret = new PspClientSecret(
                        _productionClientSecret: "Client_Secret_4bd8c21a1107a627c2db3a7dcf1c4180ce1a040a",
                        _homologationClientSecret: "Client_Secret_0ab77acbf2bde2cc40a1162f596846fa75ff710e");

                    var client = new PspClient(pspClientId, pspClientSecret);

                    var certificate = new X509Certificate2(Preference.CertificatePath);

                    var psp = new PspHomologated.Gerencianet(client, certificate);

                    new StartConfig(psp, PspEnvironment.Production);


                    var cob = new CobRequest(_chave: "1b0e2743-0769-4f21-b0b7-9cfddb2a5a2b")
                    {
                        Calendario = new CalendarioRequest
                        {
                            Expiracao = 3600
                        },
                        //Devedor = new DevedorRequest
                        //{
                        //    Cpf = "12345678909",
                        //    Nome = "Francisco da Silva",
                        //},
                        Valor = new ValorRequest
                        {
                            Original = pixPaylod.PixCob.Viewer.ValueFormatter
                        },
                        SolicitacaoPagador = pixPaylod.PixCob.Description,
                        //    InfoAdicionais = new List<InfoAdicional>
                        //{
                        //    new InfoAdicionalRequest
                        //    {
                        //        Nome = "Campo 1",
                        //        Valor = "Informação Adicional1 do PSP-Recebedor"
                        //    },
                        //    new InfoAdicionalRequest
                        //    {
                        //        Nome = "Campo 2",
                        //        Valor = "Informação Adicional2 do PSP-Recebedor"
                        //    }
                        //}
                    };

                    var cobRequest = new CobRequestService();

                    pixPaylod.PixDynamicCob = await cobRequest.Create(Guid.NewGuid().ToString("N"), cob).ConfigureAwait(false);

                    var payload = pixPaylod.PixDynamicCob.ToDynamicPayload(pixPaylod.PixDynamicCob.Txid, new Merchant(pixPaylod.PixKey.Name, pixPaylod.PixKey.City), pixPaylod.PixDynamicCob.Location);

                    var stringToQrCode = payload.GenerateStringToQrCode();

                    pixPaylod.QrCode = stringToQrCode;
                }

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
