using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Commands.Interfaces
{
    public interface IPixPayloadCommand
    {
        PixPayloadCommand Create(PixPayload pixPayload);
    }
}
