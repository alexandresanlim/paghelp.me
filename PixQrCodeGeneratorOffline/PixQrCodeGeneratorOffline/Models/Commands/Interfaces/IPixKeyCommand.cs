using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Commands.Interfaces
{
    public interface IPixKeyCommand
    {
        PixKeyCommand Create(PixKey pixKey);
    }
}
