using Plugin.StoreReview;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PixQrCodeGeneratorOffline
{
    public partial class App
    {
        public const string AppName = "Paghelp";

        public const string IconName = "pixoff";

        public static class Info
        {
            //Fundação do paghelpe.me
            //public static string Date => new DateTime(2020, 12, 28).ToString("dd MMM yyyy");

            public static string Date => new DateTime(2021, 06, 27).ToString("dd MMM yyyy");

            public static string AppName => AppInfo.Name;

            public static string Build => AppInfo.BuildString;

            public static string VersionString => AppInfo.VersionString;

            public static string PackageName => AppInfo.PackageName;

            public static string AndroidId => PackageName;

            public static string GooglePlayLink => $"https://play.google.com/store/apps/details?id={PackageName}";

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

            public static string InstagramUsername => "paghelp.me";

            public static string InstagramLink => $"https://www.instagram.com/{InstagramUsername}";
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
                    return EviromentType.Development;
#else
                    return EviromentType.Production;
#endif
                }
            }

            public static bool IsProduction => Current == EviromentType.Production;

            public static bool IsDevelopment => Current == EviromentType.Development;
        }

        public static class Ids
        {
            public static string AppCenter => Evironment.IsDevelopment ? "ecf1ffa0 - fad8 - 47f3-984e-c59dcdb24c29" : "b0e08456-a911-48da-b391-33daf270896c";

            public static string GoogleAds => DeviceInfo.IsAndroid ?
#if DEBUG
                "ca-app-pub-3940256099942544/6300978111" : "ca-app-pub-3940256099942544/2934735716";
#else
                "ca-app-pub-1328926374682196/6888131347" : "";
#endif
        }

        public static void OpenAppInStore() => CrossStoreReview.Current.OpenStoreReviewPage(Info.PackageName);

        public static async Task RequestReview()
        {
            try
            {
                await CrossStoreReview.Current.RequestReview(false);
            }
            catch (Exception)
            {
                OpenAppInStore();
            }
        }

        public static async Task OpenAppIntagram()
        {
            var supportsUri = await Launcher.CanOpenAsync("instagram://");

            if (supportsUri)
                await Launcher.OpenAsync(new Uri("instagram://user?username=" + Info.InstagramUsername));

            else
                await Launcher.OpenAsync(new Uri(Info.InstagramLink));
        }
    }
}
