using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace PixQrCodeGeneratorOffline.Services
{
    public static class Preference
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

        public static bool ShowInList
        {
            get => Preferences.Get(nameof(ShowInList), false);
            set
            {
                if (ShowInList == value)
                    return;

                Preferences.Set(nameof(ShowInList), value);
            }
        }

        public static bool FingerPrint
        {
            get => Preferences.Get(nameof(FingerPrint), false);
            set
            {
                if (FingerPrint == value)
                    return;

                Preferences.Set(nameof(FingerPrint), value);
            }
        }

        public static bool IsPDVMode
        {
            get => Preferences.Get(nameof(IsPDVMode), false);
            set
            {
                if (IsPDVMode == value)
                    return;

                Preferences.Set(nameof(IsPDVMode), value);
            }
        }

        public static bool ShowNews
        {
            get => Preferences.Get(nameof(ShowNews), true);
            set
            {
                if (ShowNews == value)
                    return;

                Preferences.Set(nameof(ShowNews), value);
            }
        }

        public static bool HaveSeenWelcome
        {
            get => Preferences.Get(nameof(HaveSeenWelcome), false);
            set
            {
                if (HaveSeenWelcome == value)
                    return;

                Preferences.Set(nameof(HaveSeenWelcome), value);
            }
        }

        public static bool ThemeIsDark
        {
            get => Preferences.Get(nameof(ThemeIsDark), AppInfo.RequestedTheme == AppTheme.Dark);
            set
            {
                if (ThemeIsDark == value)
                    return;

                Preferences.Set(nameof(ThemeIsDark), value);
            }
        }
    }
}
