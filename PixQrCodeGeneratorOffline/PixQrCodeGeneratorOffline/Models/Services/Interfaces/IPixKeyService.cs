using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IPixKeyService
    {
        bool IsValid(PixKey pixKey);
    }
}
