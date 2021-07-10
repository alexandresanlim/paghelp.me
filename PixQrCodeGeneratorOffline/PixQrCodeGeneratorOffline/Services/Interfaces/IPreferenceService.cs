using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IPreferenceService
    {
        void ChangeHideData();

        void ChangeShowInList();

        Task ChangeFingerPrint();

        Task ChangePDVMode();
    }
}
