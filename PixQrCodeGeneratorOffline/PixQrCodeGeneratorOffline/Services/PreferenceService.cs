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
            var newData = !Preference.HideData;
            Preference.HideData = newData;
        }

        public void ChangeShowInList()
        {
            var newData = !Preference.ShowInList;
            Preference.ShowInList = newData;
        }

        public void ChangeFingerPrint()
        {
            var newData = !Preference.FingerPrint;
            Preference.ShowInList = newData;
        }
    }
}
