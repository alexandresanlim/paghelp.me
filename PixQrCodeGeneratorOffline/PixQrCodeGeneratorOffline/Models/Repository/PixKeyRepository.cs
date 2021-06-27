using LiteDB;
using PixQrCodeGeneratorOffline.Models.Repository.Base;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Repository
{
    public class PixKeyRepository : BaseDatabase, IPixKeyRepository
    {
        //public static ILiteCollection<PixKey> ItemCollection => GetDatabase.GetCollection<PixKey>();

        private readonly ILiteCollection<PixKey> _pixCollection;

        public PixKeyRepository()
        {
            _pixCollection = GetDatabase.GetCollection<PixKey>();
        }

        public List<PixKey> GetAll()
        {
            try
            {
                return _pixCollection.FindAll().ToList();
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                return new PixKey();
            }
        }

        public bool UpInsert(PixKey item)
        {
            try
            {
                return _pixCollection.Upsert(item.Id, item);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(PixKey item)
        {
            try
            {
                return _pixCollection.Update(item);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Insert(PixKey item)
        {
            try
            {
                return _pixCollection.Insert(item);
            }
            catch (Exception e)
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

        public bool RemoveAll()
        {
            try
            {
                return _pixCollection.DeleteAll() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
