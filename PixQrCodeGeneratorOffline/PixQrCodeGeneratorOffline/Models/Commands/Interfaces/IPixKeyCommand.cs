using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;

namespace PixQrCodeGeneratorOffline.Models.Commands.Interfaces
{
    public interface IPixKeyCommand
    {
        PixKeyCommand Create(PixKey pixKey);
    }
}
