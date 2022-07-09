using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PixQrCodeGeneratorOffline.Models.Repository.Interfaces
{
    public interface IPixKeyRepository
    {
        List<PixKey> GetAll(Expression<Func<PixKey, bool>> predicate);

        List<PixKey> GetAll();

        PixKey FindById(int id);

        bool Update(PixKey item);

        bool Insert(PixKey item);

        bool Remove(PixKey item);

        bool RemoveAll(Expression<Func<PixKey, bool>> predicate);
    }
}
