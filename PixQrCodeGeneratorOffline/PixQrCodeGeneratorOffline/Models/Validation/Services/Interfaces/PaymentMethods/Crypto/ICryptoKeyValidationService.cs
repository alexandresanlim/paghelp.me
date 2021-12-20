using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Validation.PaymentMethods.Crypto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces.PaymentMethods.Crypto
{
    public interface ICryptoKeyValidationService
    {
        CryptoKeyValidation Create(CryptoKey pixKey);
    }
}
