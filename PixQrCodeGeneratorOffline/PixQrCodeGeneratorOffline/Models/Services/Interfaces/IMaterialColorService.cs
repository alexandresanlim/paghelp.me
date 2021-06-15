using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IMaterialColorService
    {
        List<MaterialColor> GetNiceCombinationList();

        MaterialColor GetRandom();

        MaterialColor GetByCurrentResourceThemeColor();

        MaterialColor GetByCurrentDeviceTheme();

        void SetOnCurrentResourceThemeColor(MaterialColor colors);
    }
}
