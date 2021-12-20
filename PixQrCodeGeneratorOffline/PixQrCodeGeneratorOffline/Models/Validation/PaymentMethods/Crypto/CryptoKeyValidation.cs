using PixQrCodeGeneratorOffline.Models.Validation.Services.Base;

namespace PixQrCodeGeneratorOffline.Models.Validation.PaymentMethods.Crypto
{
    public class CryptoKeyValidation : ValidationBase
    {
        public bool HasKey { get; set; }

        public bool IsEdit { get; set; }
    }
}
