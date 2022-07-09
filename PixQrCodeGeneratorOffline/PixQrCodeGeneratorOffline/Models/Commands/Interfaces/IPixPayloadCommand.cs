using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;

namespace PixQrCodeGeneratorOffline.Models.Commands.Interfaces
{
    public interface IPixPayloadCommand
    {
        PixPayloadCommand Create(PixPayload pixPayload);
    }
}
