using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces
{
    public interface IPixKeyViewerService
    {
        PixKeyViewer Create(PixKey pixKey);
    }
}
