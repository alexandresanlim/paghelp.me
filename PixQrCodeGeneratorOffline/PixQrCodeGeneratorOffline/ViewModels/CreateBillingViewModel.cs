using PixQrCodeGeneratorOffline.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class CreateBillingViewModel : BaseViewModel
    {
        public CreateBillingViewModel()
        {
            //LoadDataCommand.Execute(null);
        }

        public Command<PixKey> LoadDataCommand => new Command<PixKey>((pixKey) =>
        {
            CurrentPixKey = pixKey;
        });

        public ICommand SharePayloadCommand => new Command(async () =>
        {
            var options = new List<Acr.UserDialogs.ActionSheetOption>
            {
                new Acr.UserDialogs.ActionSheetOption("Copiar Código", async () =>
                {
                   await CopyText(CurrentPixKey.Payload, "Código copiado com sucesso!");
                }),
                new Acr.UserDialogs.ActionSheetOption("Compartilhar Código", async () =>
                {
                    await ShareText(CurrentPixKey?.Payload);
                }),
            };

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "O que deseja fazer?",
                //UseBottomSheet = true,
                Options = options,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        });

        public ICommand AddValueCommand => new Command(() =>
        {
            var initial = CurrentPixKey.Value ?? "0";

            var d = decimal.Parse(initial);
            d = d += 1;

            CurrentPixKey.Value = d.ToString();

            CurrentPixKey.RaisePresentation();
        });

        public ICommand RmValueCommand => new Command(() =>
        {
            if (string.IsNullOrEmpty(CurrentPixKey?.Value) || CurrentPixKey.Value == "0")
            {
                CurrentPixKey.Value = "";
                return;
            }

            var initial = CurrentPixKey.Value;

            var d = decimal.Parse(initial);
            d = d -= 1;

            CurrentPixKey.Value = d.ToString();

            CurrentPixKey.RaisePresentation();
        });

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }

        private string _currentQrCode;
        public string CurrentQrCode
        {
            set => SetProperty(ref _currentQrCode, value);
            get => _currentQrCode;
        }
    }
}
