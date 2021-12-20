using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
