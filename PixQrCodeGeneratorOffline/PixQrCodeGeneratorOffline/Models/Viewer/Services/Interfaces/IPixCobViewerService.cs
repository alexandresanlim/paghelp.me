using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces
{
    public interface IPixCobViewerService
    {
        PixCobViewer Create(PixCob pixCob);
    }
}
