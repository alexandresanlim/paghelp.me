using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;

namespace PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto.Interfaces
{
    public interface ICryptoPayloadCommand
    {
        CryptoPayloadCommand Create(CryptoPayload pixPayload);
    }
}
