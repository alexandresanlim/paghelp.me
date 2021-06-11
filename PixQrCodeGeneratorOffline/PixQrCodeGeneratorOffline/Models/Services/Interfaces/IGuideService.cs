using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IGuideService
    {
        Guide Create(string _question, string _answer);
    }
}
