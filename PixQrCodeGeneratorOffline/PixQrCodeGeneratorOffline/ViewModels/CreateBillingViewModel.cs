using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class CreateBillingViewModel : ViewModelBase
    {
        public string AddDescriptionValue => "Adicionar Descrição";

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

                LoadPixPayloadSave();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        });

        public ICommand IsBillingVisibleCommand => new Command(() => IsBillingSaveVisible = !IsBillingSaveVisible);

        private void LoadPixPayloadSave()
        {
            RecentPixCobList = _pixPayloadService?.GetAll(x => x.PixKey.Id == CurrentPixKey.Id)?.ToObservableCollection() ?? new ObservableCollection<PixPayload>();
        }

        public Command<string> InputTextCommand => new Command<string>(async (text) =>
        {
            try
            {
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

            var finalString = System.Convert.ToDecimal(d, new System.Globalization.CultureInfo("en-US")).ToString("N");

            CurrentCob.Value = finalString;
        }

        public string ValueInput { get; set; }

        public ICommand NavigateToPaymentPageCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(500);

                var pixPaylod = _pixPayloadService.Create(CurrentPixKey, CurrentCob);

                if (!_pixPayloadService.IsValid(pixPaylod))
                    return;

                pixPaylod.Commands.NavigateToPaymentPageCommand.Execute(null);
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

        public ICommand SetDescriptionCommand => new Command(async () =>
        {
            try
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

        private ObservableCollection<PixPayload> _recentPixCobList;
        public ObservableCollection<PixPayload> RecentPixCobList
        {
            set => SetProperty(ref _recentPixCobList, value);
            get => _recentPixCobList;
        }

        private bool _isBillingSaveVisible;
        public bool IsBillingSaveVisible
        {
            set => SetProperty(ref _isBillingSaveVisible, value);
            get => _isBillingSaveVisible;
        }
    }
}
