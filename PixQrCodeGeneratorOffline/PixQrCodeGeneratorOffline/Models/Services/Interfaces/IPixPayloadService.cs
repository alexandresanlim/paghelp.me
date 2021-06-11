using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IPixPayloadService
    {
        PixPayload Create(PixKey pixKey);

        PixPayload Create(PixKey pixKey, PixCob pixCob);

        bool IsValid(PixPayload pixPayload);
    }
}
