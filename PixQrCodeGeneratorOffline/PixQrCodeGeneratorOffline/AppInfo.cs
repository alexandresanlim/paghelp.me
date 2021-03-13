using PixQrCodeGeneratorOffline.Style;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline
{
    public partial class App
    {
        public const string AppName = "PIX APP";

        public const string IconName = "pixoff";

        public static class Info
        {
            public static string Date => new DateTime(2020, 12, 28).ToString("dd MMM yyyy");

            public static string AppName => AppInfo.Name;

            public static string Build => AppInfo.BuildString;

            public static string VersionString => AppInfo.VersionString;

            public static string PackageName => AppInfo.PackageName;

            public static string AndroidId => PackageName;

            public static string GooglePlayLink => "https://play.google.com/store/apps/details?id=" + PackageName;

            public static string AppStoreLink => "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id={" + PackageName + "}&amp;onlyLatestVersion=true&amp;pageNumber=0&amp;sortOrdering=1&amp;type=Purple+Software";

            public static string StoreTextToShare
            {
                get
                {
                    var text = "Estou usando e indico instalar o app " + AppName + "\n\n";

                    text += "Link para Play Store (Android): " + GooglePlayLink + "\n";
                    text += "Link para App Store (iOS): " + AppStoreLink;

                    return text;
                }
            }

            public static string StoreNameByDeviceInfo => DeviceInfo.IsAndroid ? "Google Play" : "App Store";

            public static string InstagramUsername => "pixoff.app";

            public static string InstagramLink => "https://www.instagram.com/" + InstagramUsername;
        }

        public static class Ads
        {
            public static string AdsId => DeviceInfo.IsAndroid ?
#if DEBUG
                "ca-app-pub-3940256099942544/6300978111" : "ca-app-pub-3940256099942544/2934735716";
#else
                "ca-app-pub-1328926374682196/6888131347" : "";
#endif
        }

        public static class Evironment
        {
            public enum EviromentType
            {
                Development,
                Production
            }

            public static EviromentType Current
            {
                get
                {
#if DEBUG
                    return EviromentType.Production;
#else
                    return EviromentType.Production;
#endif
                }
            }

            public static bool IsProduction => (Current == EviromentType.Production);

            public static bool IsDevelopment => (Current == EviromentType.Development);
        }

        public static async Task OpenAppInStore()
        {
            var supportsUri = await Launcher.CanOpenAsync("market://");

            if (supportsUri)
                await Launcher.OpenAsync(new Uri("market://details?id=" + Info.PackageName));

            else
            {
                var storeLink = Device.RuntimePlatform == Device.Android ? Info.GooglePlayLink : Info.AppStoreLink;
                await Launcher.OpenAsync(new Uri(storeLink));
            }
        }

        public static async Task OpenAppIntagram()
        {
            var supportsUri = await Launcher.CanOpenAsync("instagram://");

            if (supportsUri)
                await Launcher.OpenAsync(new Uri("instagram://user?username=" + Info.InstagramUsername));

            else
            {
                await Launcher.OpenAsync(new Uri(Info.InstagramLink));
            }
        }
    }
}
