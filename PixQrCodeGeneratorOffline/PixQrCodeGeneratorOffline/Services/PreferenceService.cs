using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Services
{
    public class PreferenceService : IPreferenceService
    {
        public void ChangeHideData()
        {
            Preference.HideData = !Preference.HideData;
        }

        public void ChangeShowInList()
        {
            Preference.ShowInList = !Preference.ShowInList;
        }

        public void ChangeFingerPrint()
        {
            Preference.FingerPrint = !Preference.FingerPrint;
        }
    }
}
