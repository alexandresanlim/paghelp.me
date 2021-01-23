using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class CreateBillingViewModel : BaseViewModel
    {
        public Command<PixKey> LoadDataCommand => new Command<PixKey>((pixKey) =>
        {
            CurrentPixKey = pixKey;
            ResetCurrentValue();
        });

        public string AddDescriptionValue => "Adicionar Descrição";

        private void ResetCurrentValue()
        {
            ValueInput = "";
            CurrentDescription = AddDescriptionValue;
            SetValueCurrencyFormat();
        }

        public Command<string> InputTextCommand => new Command<string>(async (text) =>
        {
            if (string.IsNullOrEmpty(text))
                ValueInput = ValueInput.RemoveLastChar();

            else
                ValueInput += text;

            SetValueCurrencyFormat();
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

            var finalString = System.Convert.ToDecimal(d, new System.Globalization.CultureInfo("en-US")).ToString("N");

            if (finalString.Length > 11)
                return;

            CurrentPixKey.Value = finalString;
        }

        public string ValueInput { get; set; }

        public ICommand NavigateToPaymentPageCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                await NavigateAsync(new PaymentPage(CurrentPixKey));
            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {
                SetIsLoading(false);
            }
        });

        public ICommand SetDescriptionCommand => new Command(async () =>
        {
            var promptConfig = new Acr.UserDialogs.PromptConfig
            {
                CancelText = "Cancelar",
                OkText = "Ok",
                Title = "Descrição",
                Message = "Digite o texto que aparecerá para o pagador",
                Placeholder = "Pedido 1",
                Text = !CurrentDescription.Equals(AddDescriptionValue) ? CurrentDescription : "",
                OnAction = new Action<Acr.UserDialogs.PromptResult>((result) =>
                {
                    if (!result.Ok)
                        return;

                    var text = result?.Text;

                    if (string.IsNullOrEmpty(text))
                    {
                        CurrentDescription = AddDescriptionValue;
                        CurrentPixKey.Description = "";
                        return;
                    }

                    CurrentDescription = text;
                    CurrentPixKey.Description = text;
                })
            };

            DialogService.Prompt(promptConfig);
        });

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }

        private string _currentDescription;
        public string CurrentDescription
        {
            set => SetProperty(ref _currentDescription, value);
            get => _currentDescription;
        }
    }
}
