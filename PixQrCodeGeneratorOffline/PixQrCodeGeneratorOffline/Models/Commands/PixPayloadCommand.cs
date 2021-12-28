using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands
{
    public class PixPayloadCommand : CommandBase, IPixPayloadCommand
    {
        public IAsyncCommand NavigateToPaymentPageCommand { get; private set; }

        public PixPayloadCommand Create(PixPayload pixPayload)
        {
            return new PixPayloadCommand
            {
                NavigateToPaymentPageCommand = GetNavigateToPaymentPageCommand(pixPayload)
            };
        }

        private AsyncCommand GetNavigateToPaymentPageCommand(PixPayload pixPayload)
        {
            return new AsyncCommand(async () =>
            {
                try
                {
                    DialogService.ShowLoading("");

                    await Task.Delay(500);

                    await Shell.Current.Navigation.PushAsync(new PaymentPage(pixPayload));
                }
                catch (System.Exception e)
                {
                    e.SendToLog();
                }
                finally
                {
                    _eventService.SendEvent("Navegou para página de pagamento a partir do PixPaylodCommand", EventType.NAVIGATION, nameof(PixPayloadCommand));

                    DialogService.HideLoading();
                }
            });
        }
    }
}
