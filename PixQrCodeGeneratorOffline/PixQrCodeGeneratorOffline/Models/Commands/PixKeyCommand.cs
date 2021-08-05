using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands
{
    public class PixKeyCommand : CommandBase, IPixKeyCommand
    {
        private readonly IExternalActionService _externalActionService;

        private readonly IPixPayloadService _pixPayloadService;

        private readonly IPixKeyService _pixKeyService;

        public PixKeyCommand()
        {
            _externalActionService = DependencyService.Get<IExternalActionService>();
            _pixPayloadService = DependencyService.Get<IPixPayloadService>();
            _pixKeyService = DependencyService.Get<IPixKeyService>();
        }

        public ICommand CopyKeyCommand { get; private set; }

        public ICommand ShareKeyCommand { get; private set; }

        public ICommand NavigateToCreateBillingPageCommand { get; private set; }

        public ICommand NavigateToPaymentPageCommand { get; private set; }

        public ICommand EditKeyCommand { get; private set; }

        public PixKeyCommand Create(PixKey pixKey)
        {
            return pixKey.Validation.IsValid ? new PixKeyCommand
            {
                CopyKeyCommand = GetCopyKeyCommand(pixKey),
                ShareKeyCommand = GetShareKeyCommand(pixKey),
                NavigateToCreateBillingPageCommand = GetNavigateToCreateBillingCommand(pixKey),
                NavigateToPaymentPageCommand = GetNavigateToPaymentPageCommand(pixKey),
                EditKeyCommand = GetEdityKeyCommand(pixKey)
            } : new PixKeyCommand();
        }

        private Command GetCopyKeyCommand(PixKey pixKey)
        {
            return new Command(async () => await _externalActionService.CopyText(pixKey?.Key, "Chave copiada com sucesso!"));
        }

        private Command GetShareKeyCommand(PixKey pixKey)
        {
            return new Command(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await _externalActionService.ShareText(pixKey?.Key);
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Compartilhou chave", EventType.SHARE);

                    DialogService.HideLoading();
                }
            });
        }

        private Command GetNavigateToCreateBillingCommand(PixKey pixKey)
        {
            return new Command(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await Shell.Current.Navigation.PushModalAsync(new CreateBillingPage(pixKey));
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Navegou para criação de cobrança", EventType.NAVIGATION);

                    DialogService.HideLoading();
                }
            });
        }

        private Command GetNavigateToPaymentPageCommand(PixKey pixKey)
        {
            return new Command(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    var pixPaylod = _pixPayloadService.Create(pixKey);

                    await Shell.Current.Navigation.PushModalAsync(new PaymentPage(pixPaylod));
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Navegou para pagina de pagamento a partir da dashboard", EventType.NAVIGATION);

                    DialogService.HideLoading();
                }
            });
        }

        private Command GetEdityKeyCommand(PixKey pixKey)
        {
            return new Command(async () => await _pixKeyService.NavigateToEdit(pixKey, isContact: pixKey.IsContact));
        }
    }
}
