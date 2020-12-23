using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace PixQrCodeGeneratorOffline.Services
{
    public static class PreferenceService
    {
        public static bool HideData
        {
            get => Preferences.Get(nameof(HideData), false);
            set
            {
                if (HideData == value)
                    return;

                Preferences.Set(nameof(HideData), value);
            }
        }
    }
}
