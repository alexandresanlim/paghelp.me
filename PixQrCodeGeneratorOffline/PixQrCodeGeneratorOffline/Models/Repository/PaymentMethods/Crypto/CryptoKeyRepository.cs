using LiteDB;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Repository.Base;
using PixQrCodeGeneratorOffline.Models.Repository.PaymentMethods.Crypto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Repository.PaymentMethods.Crypto
{
    public class CryptoKeyRepository : BaseDatabase, ICryptoKeyRepository
    {
        private readonly ILiteCollection<CryptoKey> _cryptoCollection;

        public CryptoKeyRepository()
        {
            _cryptoCollection = GetDatabase.GetCollection<CryptoKey>();
        }

        public List<CryptoKey> GetAll(Expression<Func<CryptoKey, bool>> predicate)
        {
            try
            {
                return _cryptoCollection.Find(predicate).ToList();
            }
            catch (Exception e)
            {
                return new List<CryptoKey>();
            }
        }

        public List<CryptoKey> GetAll()
        {
            try
            {
                return _cryptoCollection.FindAll().ToList();
            }
            catch (Exception e)
            {
                return new List<CryptoKey>();
            }
        }

        public CryptoKey FindById(int id)
        {
            try
            {
                return _cryptoCollection.FindById(id);
            }
            catch (Exception e)
            {
                return new CryptoKey();
            }
        }

        public bool Update(CryptoKey item)
        {
            try
            {
                return _cryptoCollection.Update(item);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Insert(CryptoKey item)
        {
            try
            {
                return _cryptoCollection.Insert(item) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Remove(CryptoKey item)
        {
            try
            {
                return _cryptoCollection.Delete(item.Id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveAll(Expression<Func<CryptoKey, bool>> predicate)
        {
            try
            {
                return _cryptoCollection.DeleteMany(predicate) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
