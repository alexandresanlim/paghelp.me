using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PixQrCodeGeneratorOffline.Models.Repository.PaymentMethods.Crypto.Interfaces
{
    public interface ICryptoKeyRepository
    {
        List<CryptoKey> GetAll(Expression<Func<CryptoKey, bool>> predicate);

        List<CryptoKey> GetAll();

        CryptoKey FindById(int id);

        bool Update(CryptoKey item);

        bool Insert(CryptoKey item);

        bool Remove(CryptoKey item);

        bool RemoveAll(Expression<Func<CryptoKey, bool>> predicate);
    }
}
