using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Repository.Interfaces
{
    public interface IPixKeyRepository
    {
        List<PixKey> GetAll();

        PixKey FindById(int id);

        bool UpInsert(PixKey item);

        bool Update(PixKey item);

        bool Insert(PixKey item);

        bool Remove(PixKey item);

        bool RemoveAll();
    }
}
