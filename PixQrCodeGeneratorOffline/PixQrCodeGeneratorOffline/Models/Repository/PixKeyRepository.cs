using LiteDB;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.Repository.Base;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PixQrCodeGeneratorOffline.Models.Repository
{
    public class PixKeyRepository : BaseDatabase, IPixKeyRepository
    {
        private readonly ILiteCollection<PixKey> _pixCollection;

        public PixKeyRepository()
        {
            _pixCollection = GetDatabase.GetCollection<PixKey>();
        }

        public List<PixKey> GetAll(Expression<Func<PixKey, bool>> predicate)
        {
            try
            {
                return _pixCollection.Find(predicate).ToList();
            }
            catch (Exception)
            {
                return new List<PixKey>();
            }
        }

        public List<PixKey> GetAll()
        {
            try
            {
                return _pixCollection.FindAll().ToList();
            }
            catch (Exception)
            {
                return new List<PixKey>();
            }
        }

        public PixKey FindById(int id)
        {
            try
            {
                return _pixCollection.FindById(id);
            }
            catch (Exception)
            {
                return new PixKey();
            }
        }

        public bool Update(PixKey item)
        {
            try
            {
                return _pixCollection.Update(item);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Insert(PixKey item)
        {
            try
            {
                return _pixCollection.Insert(item) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(PixKey item)
        {
            try
            {
                return _pixCollection.Delete(item.Id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveAll(Expression<Func<PixKey, bool>> predicate)
        {
            try
            {
                return _pixCollection.DeleteMany(predicate) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
