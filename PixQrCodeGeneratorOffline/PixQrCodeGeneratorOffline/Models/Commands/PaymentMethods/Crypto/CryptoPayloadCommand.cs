using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto
{
    public class CryptoPayloadCommand : CommandBase, ICryptoPayloadCommand
    {
        public ICommand NavigateToPaymentPageCommand { get; private set; }

        CryptoPayloadCommand ICryptoPayloadCommand.Create(CryptoPayload pixPayload)
        {
            return new CryptoPayloadCommand
            {
                NavigateToPaymentPageCommand = GetNavigateToPaymentPageCommand(pixPayload)
            };
        }

        private Command GetNavigateToPaymentPageCommand(CryptoPayload pixPayload)
        {
            return new Command(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    //await Shell.Current.Navigation.PushAsync(new PaymentPage(pixPayload), true);
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Navegou para página de pagamento a partir do PixPaylodCommand", EventType.NAVIGATION);

                    DialogService.HideLoading();
                }
            });
        }
    }
}
