using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IPreferenceService
    {
        void ChangeHideData();

        void ChangeShowInList();

        void ChangeFingerPrint();
    }
}
