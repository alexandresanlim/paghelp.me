using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IPixKeyService
    {
        bool IsValid(PixKey pixKey);

        List<PixKey> GetAll();

        PixKey GetFirst();

        bool Update(PixKey item);

        bool Insert(PixKey item);

        bool Remove(PixKey item);

        bool RemoveAll();
    }
}
