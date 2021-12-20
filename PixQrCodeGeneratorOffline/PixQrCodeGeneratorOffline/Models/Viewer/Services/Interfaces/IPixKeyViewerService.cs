using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;

namespace PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces
{
    public interface IPixKeyViewerService
    {
        PixKeyViewer Create(PixKey pixKey);
    }
}
