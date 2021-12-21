using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto.Interfaces
{
    public interface ICryptoPayloadCommand
    {
        CryptoPayloadCommand Create(CryptoPayload pixPayload);
    }
}
