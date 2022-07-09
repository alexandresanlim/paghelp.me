using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;

namespace PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services.Interfaces
{
    public interface ICryptoKeyViewerService
    {
        CryptoKeyViewer Create(CryptoKey pixKey);
    }
}
