using LiteDB;
using PixQrCodeGeneratorOffline.Models.Repository.Base;
using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PixQrCodeGeneratorOffline.Models.Repository
{
    public class PixPayloadRepository : BaseDatabase, IPixPayloadRepository
    {
        private readonly ILiteCollection<PixPayload> _collection;

        public PixPayloadRepository()
        {
            _collection = GetDatabase.GetCollection<PixPayload>();
        }

        public PixPayload FindById(int id)
        {
            try
            {
                return _collection.FindById(id);
            }
            catch (Exception e)
            {
                return new PixPayload();
            }
        }

        public List<PixPayload> GetAll(Expression<Func<PixPayload, bool>> predicate = null)
        {
            try
            {
                return predicate == null ? _collection.FindAll().ToList() : _collection.Find(predicate).ToList();
            }
            catch (Exception e)
            {
                return new List<PixPayload>();
            }
        }

        public bool Insert(PixPayload item)
        {
            try
            {
                return _collection.Insert(item) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Remove(PixPayload item)
        {
            try
            {
                return _collection.Delete(item.Id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveAll(Expression<Func<PixPayload, bool>> predicate = null)
        {
            try
            {
                if (predicate != null)
                    return _collection.DeleteMany(predicate) > 0;

                else
                    return _collection.DeleteAll() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(PixPayload item)
        {
            try
            {
                return _collection.Update(item);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
