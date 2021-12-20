using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;

namespace PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces
{
    public interface IPixCobViewerService
    {
        PixCobViewer Create(PixCob pixCob);
    }
}
