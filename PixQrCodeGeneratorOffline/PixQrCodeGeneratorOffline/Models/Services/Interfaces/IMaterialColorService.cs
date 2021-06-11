using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IMaterialColorService
    {
        List<MaterialColor> GetNiceCombinationList();

        MaterialColor GetRandom();

        MaterialColor GetLightColors();

        MaterialColor GetDarkColors();

        void SetOnCurrentResourceThemeColor(MaterialColor colors);

        MaterialColor GetByCurrentResourceThemeColor();
    }
}
