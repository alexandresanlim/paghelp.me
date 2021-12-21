using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto.Interfaces
{
    public interface ICryptoPayloadService
    {
        CryptoPayload Create(CryptoKey pixKey);

        CryptoPayload Create(CryptoKey pixKey, CryptoCob pixCob);

        bool IsValid(CryptoPayload pixPayload);

        bool Save(CryptoPayload pixPaylod);

        List<CryptoPayload> GetAll(Expression<Func<CryptoPayload, bool>> predicate = null);

        Task<bool> RemoveAll(Expression<Func<CryptoPayload, bool>> predicate = null);
    }
}
