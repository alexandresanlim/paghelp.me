using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IPixCobService
    {
        PixCob Create(string value, string description = "");
    }
}
