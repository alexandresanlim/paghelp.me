using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces
{
    public interface IPixCobValidationService
    {
        PixCobValidation Create(PixCob pixCob);
    }
}
