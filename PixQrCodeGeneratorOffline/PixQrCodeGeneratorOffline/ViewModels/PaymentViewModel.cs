using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        #region Commands

        public ICommand SharePayloadCommand => new Command(SharePayload);

        public IAsyncCommand SaveCommand => new AsyncCommand(Save);

        #endregion

        public ICommand LoadDataCommand => new Command<PixPayload>((pixPaylod) =>
        {
            CurrentPixPaylod = pixPaylod;

            //SaveButtonVisible = !(CurrentPixPaylod.Id > 0) && CurrentPixPaylod?.PixCob != null && CurrentPixPaylod.PixCob.Validation.HasValue;
        });

        private void SharePayload()
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
                _eventService.SendEvent("Compartilhou um payload", Services.EventType.SHARE);
            }
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
                await DashboardVM.LoadBilling();
            }

            else
            {
                DialogService.Toast("Algo de errado aconteceu, tente novamente mais tarde ou atualize o app.");
            }
        }

        private PixPayload _currentPixPaylod;
        public PixPayload CurrentPixPaylod
        {
            set => SetProperty(ref _currentPixPaylod, value);
            get => _currentPixPaylod;
        }

        private bool _saveButtonVisible;
        public bool SaveButtonVisible
        {
            set => SetProperty(ref _saveButtonVisible, value);
            get => _saveButtonVisible;
        }
    }
}
