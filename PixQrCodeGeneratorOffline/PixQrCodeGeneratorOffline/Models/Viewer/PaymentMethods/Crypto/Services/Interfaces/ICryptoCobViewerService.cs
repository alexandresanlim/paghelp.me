using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;

namespace PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services.Interfaces
{
    public interface ICryptoCobViewerService
    {
        CryptoCobViewer Create(CryptoCob pixCob);
    }
}
