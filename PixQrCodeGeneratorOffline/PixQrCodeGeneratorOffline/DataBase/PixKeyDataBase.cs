using LiteDB;
using PixQrCodeGeneratorOffline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixQrCodeGeneratorOffline.DataBase
{
    public abstract class PixKeyDataBase : BaseDatabase
    {
        public static ILiteCollection<PixKey> ItemCollection => GetDatabase.GetCollection<PixKey>();

        public static List<PixKey> GetAll()
        {
            try
            {
                return ItemCollection.FindAll().ToList();
            }
            catch (Exception e)
            {
                return new List<PixKey>();
            }
        }

        public static bool UpInsert(PixKey item)
        {
            try
            {
                return ItemCollection.Upsert(item.Id, item);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool Update(PixKey item)
        {
            try
            {
                return ItemCollection.Update(item);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool Insert(PixKey item)
        {
            try
            {
                return ItemCollection.Insert(item);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool Remove(PixKey item)
        {
            try
            {
                return ItemCollection.Delete(item.Id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool RemoveAll()
        {
            try
            {
                return ItemCollection.DeleteAll() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
