using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Repository.Interfaces
{
    public interface IPixKeyRepository
    {
        List<PixKey> GetAll();

        List<PixKey> GetAll(Expression<Func<PixKey, bool>> predicate);

        PixKey FindById(int id);

        bool UpInsert(PixKey item);

        bool Update(PixKey item);

        bool Insert(PixKey item);

        bool Remove(PixKey item);

        bool RemoveAll();

        bool RemoveAll(Expression<Func<PixKey, bool>> predicate);
    }
}
