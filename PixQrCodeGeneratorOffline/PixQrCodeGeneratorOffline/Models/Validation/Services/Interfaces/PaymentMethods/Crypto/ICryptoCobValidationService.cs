using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Validation.PaymentMethods.Crypto;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces.PaymentMethods.Crypto
{
    public interface ICryptoCobValidationService
    {
        CryptoCobValidation Create(CryptoCob pixCob);
    }
}
