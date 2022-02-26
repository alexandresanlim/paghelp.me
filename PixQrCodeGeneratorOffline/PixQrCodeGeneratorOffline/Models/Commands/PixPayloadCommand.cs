using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Views;
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
                NavigateToPaymentPageCommand = GetNavigateToPaymentPageCommand(pixPayload),
            };
        }

        private IAsyncCommand GetNavigateToPaymentPageCommand(PixPayload pixPayload) =>
            _customAsyncCommand.Create(async () => await Shell.Current.Navigation.PushAsync(new PaymentPage(pixPayload)));
    }
}
