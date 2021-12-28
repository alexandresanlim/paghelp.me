using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;

namespace PixQrCodeGeneratorOffline.Models.Commands.Base
{
    public class PayloadCommandBase : CommandBase, IPayloadCommandBase
    {
        public IAsyncCommand ShareCommand { get; private set; }

        public IAsyncCommand CopyCommand { get; private set; }

        public IAsyncCommand ShareOnWhatsAppCommand { get; private set; }

        public PayloadCommandBase Create(PayloadBase pixPayload)
        {
            return new PayloadCommandBase
            {
                ShareCommand = GetShareCommand(pixPayload),
                CopyCommand = GetCopyCommand(pixPayload),
                ShareOnWhatsAppCommand = GetShareOnWhatsAppCommand(pixPayload),
            };
        }

        private AsyncCommand GetShareCommand(PayloadBase pixPayload)
        {
            return new AsyncCommand(async () =>
            {
                await _externalActionService.ShareText(pixPayload?.QrCode);
            });
        }

        private AsyncCommand GetCopyCommand(PayloadBase pixPayload)
        {
            return new AsyncCommand(async () =>
            {
                await _externalActionService.CopyText(pixPayload?.QrCode, "Código copiado com sucesso!");
            });
        }

        private AsyncCommand GetShareOnWhatsAppCommand(PayloadBase pixPayload)
        {
            return new AsyncCommand(async () =>
            {
                await _externalActionService.ShareOnWhats(pixPayload?.QrCode);
            });
        }
    }
}
