using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Enums;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions
{
    public static class PixKeyExtention
    {
        public static bool IsAKey(this string key)
        {
            return GetKeyType(key) != PixKeyType.NotFound;
        }

        public static PixKeyType GetKeyType(this string key)
        {
            try
            {
                if (key.IsEmail())
                    return PixKeyType.Email;

                if (key.IsCPF())
                    return PixKeyType.CPF;

                if (key.IsCNPJ())
                    return PixKeyType.CNPJ;

                if (key.IsGuid())
                    return PixKeyType.Aleatoria;

                return PixKeyType.NotFound;
            }
            catch (System.Exception)
            {
                return PixKeyType.NotFound;
            }
        }

        public static bool IsValid(this PixKey pixKey) => pixKey != null && !string.IsNullOrWhiteSpace(pixKey?.Key);

        public static bool HasKey(this PixKey pixkey) => !string.IsNullOrWhiteSpace(pixkey?.Key);

        public static bool HasName(this PixKey pixKey) => !string.IsNullOrWhiteSpace(pixKey?.Name);

        public static bool IsEdit(this PixKey pixKey) => !string.IsNullOrEmpty(pixKey?.Key);
    }
}
