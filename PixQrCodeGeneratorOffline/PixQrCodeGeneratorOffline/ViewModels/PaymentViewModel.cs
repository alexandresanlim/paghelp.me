using PixQrCodeGeneratorOffline.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        public Command<PixKey> LoadDataCommand => new Command<PixKey>((pixKey) =>
        {
            CurrentPixKey = pixKey;
            CurrentPixKey.RaiseCob();
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

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }
    }
}
