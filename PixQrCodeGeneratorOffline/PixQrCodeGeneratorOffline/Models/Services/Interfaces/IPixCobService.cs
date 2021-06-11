using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IPixCobService
    {
        PixCob Create(string value, string description = "");

        bool IsValid(PixCob pixCob);
    }
}
