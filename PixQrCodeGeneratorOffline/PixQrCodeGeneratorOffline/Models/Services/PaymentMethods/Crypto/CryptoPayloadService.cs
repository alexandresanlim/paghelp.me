using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto
{
    public class CryptoPayloadService : ICryptoPayloadService
    {
        public CryptoPayload Create(CryptoKey pixKey)
        {
            return null;
            //throw new NotImplementedException();
        }

        public CryptoPayload Create(CryptoKey pixKey, CryptoCob pixCob)
        {
            return null;
            //throw new NotImplementedException();
        }

        public List<CryptoPayload> GetAll(Expression<Func<CryptoPayload, bool>> predicate = null)
        {
            return null;
            //throw new NotImplementedException();
        }

        public bool IsValid(CryptoPayload pixPayload)
        {
            return false;
            //throw new NotImplementedException();
        }

        public Task<bool> RemoveAll(Expression<Func<CryptoPayload, bool>> predicate = null)
        {
            return Task.FromResult(false);
            //throw new NotImplementedException();
        }

        public bool Save(CryptoPayload pixPaylod)
        {
            return false;
            //throw new NotImplementedException();
        }
    }
}
