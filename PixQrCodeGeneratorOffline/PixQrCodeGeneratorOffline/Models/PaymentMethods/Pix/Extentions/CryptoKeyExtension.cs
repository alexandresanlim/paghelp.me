using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions
{
    public static class CryptoKeyExtension
    {
        public static bool IsValid(this CryptoKey pixKey)
        {
            return pixKey != null && !string.IsNullOrWhiteSpace(pixKey?.Key);
        }

        public static bool HasKey(this CryptoKey pixKey)
        {
            return !string.IsNullOrWhiteSpace(pixKey?.Key);
        }

        public static bool IsEdit(this CryptoKey pixKey)
        {
            return !string.IsNullOrEmpty(pixKey?.Key);
        }
    }
}
