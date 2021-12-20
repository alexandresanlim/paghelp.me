using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces
{
    public interface IPixKeyValidationService
    {
        PixKeyValidation Create(PixKey pixKey);
    }
}
