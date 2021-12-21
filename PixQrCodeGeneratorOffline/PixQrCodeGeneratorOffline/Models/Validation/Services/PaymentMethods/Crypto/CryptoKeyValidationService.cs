using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Validation.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces.PaymentMethods.Crypto;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services.PaymentMethods.Crypto
{
    internal class CryptoKeyValidationService : ICryptoKeyValidationService
    {
        public CryptoKeyValidation Create(CryptoKey pixKey)
        {
            return new CryptoKeyValidation
            {
                IsValid = GetIsValid(pixKey),
                HasKey = GetHasKey(pixKey),
                IsEdit = GetIsEdit(pixKey)
            };
        }

        private bool GetIsValid(CryptoKey pixKey)
        {
            return pixKey != null && !string.IsNullOrWhiteSpace(pixKey?.Key);
        }

        private bool GetHasKey(CryptoKey pixKey)
        {
            return !string.IsNullOrWhiteSpace(pixKey?.Key);
        }

        private bool GetIsEdit(CryptoKey pixKey)
        {
            return !string.IsNullOrEmpty(pixKey?.Key);
        }
    }
}
