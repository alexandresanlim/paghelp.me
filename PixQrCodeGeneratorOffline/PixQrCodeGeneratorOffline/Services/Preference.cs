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

        public static bool CryptoAble
        {
            get => Preferences.Get(nameof(CryptoAble), false);
            set
            {
                if (CryptoAble == value)
                    return;

                Preferences.Set(nameof(CryptoAble), value);
            }
        }

        public static int AreYouLikingAppMsgCount
        {
            get => Preferences.Get(nameof(AreYouLikingAppMsgCount), 0);
            set
            {
                if (AreYouLikingAppMsgCount == value)
                    return;

                Preferences.Set(nameof(AreYouLikingAppMsgCount), value);
            }
        }

        public static bool LikingAppMsgWasShowed
        {
            get => Preferences.Get(nameof(LikingAppMsgWasShowed), false);
            set
            {
                if (LikingAppMsgWasShowed == value)
                    return;

                Preferences.Set(nameof(LikingAppMsgWasShowed), value);
            }
        }

    }
}
