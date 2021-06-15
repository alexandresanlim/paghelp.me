using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        public Command<PixPayload> LoadDataCommand => new Command<PixPayload>((pixPaylod) =>
        {
            CurrentPixPaylod = pixPaylod;
        });

        public ICommand SharePayloadCommand => new Command(async () =>
        {
            try
            {
                var options = new List<Acr.UserDialogs.ActionSheetOption>
                {
                    new Acr.UserDialogs.ActionSheetOption("Copiar Código", async () =>
                    {
                       await _externalActionService.CopyText(CurrentPixPaylod?.QrCode, "Código copiado com sucesso!");
                    }),
                    new Acr.UserDialogs.ActionSheetOption("Compartilhar Código", async () =>
                    {
                        await _externalActionService.ShareText(CurrentPixPaylod?.QrCode);
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
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Compartilhou um payload");
            }
        });

        private PixPayload _currentPixPaylod;
        public PixPayload CurrentPixPaylod
        {
            set => SetProperty(ref _currentPixPaylod, value);
            get => _currentPixPaylod;
        }
    }
}
