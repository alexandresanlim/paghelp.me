using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IMaterialColorService
    {
        MaterialColor GetRandom();

        MaterialColor GetByCurrentDeviceTheme();

        void SetOnCurrentResource(MaterialColor colors);

        MaterialColor GetOnCurrentResource();
    }
}
