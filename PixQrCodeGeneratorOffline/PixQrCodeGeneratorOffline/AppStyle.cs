using PixQrCodeGeneratorOffline.Style;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline
{
    public partial class App
    {
        //public static MaterialColor Style { get; private set; }

        public static void LoadTheme(MaterialColor theme = null, bool isShowInList = false)
        {
            var themeOrRandom = theme ?? MaterialColor.GetRandom();

            //App.Style = themeOrRandom;

            MaterialColor.SetOnCurrentResourceThemeColor(themeOrRandom);

            var service = DependencyService.Get<IStatusBar>();
            service?.SetStatusBarColor(!isShowInList ? themeOrRandom.Primary : themeOrRandom.PrimaryDark);

            //var service = DependencyService.Get<IStatusBar>();
            //service?.SetStatusBarColor(ThemeColors.BackgroundPage);
        }

        public static MaterialColor ThemeColors => MaterialColor.GetByCurrentResourceThemeColor();

        public class DeviceInfo
        {
            // Manufacturer (Samsung)
            public static string Manufacturer => Xamarin.Essentials.DeviceInfo.Manufacturer;

            // Device Model (SMG-950U, iPhone10,6)
            public static string Model => Xamarin.Essentials.DeviceInfo.Model;

            public static string ManufacturerAndModel => Manufacturer + " " + Model;

            // Device Name (Motz's iPhone)
            public static string DeviceName => Xamarin.Essentials.DeviceInfo.Name;

            // Operating System Version Number (7.0)
            public static string Version => Xamarin.Essentials.DeviceInfo.VersionString;

            // Platform (Android)
            public static Xamarin.Essentials.DevicePlatform Platform => Xamarin.Essentials.DeviceInfo.Platform;

            public static string PlatformAndVersion => Platform.ToString() + " " + Version;

            // Idiom (Phone)
            public static Xamarin.Essentials.DeviceIdiom Idiom => Xamarin.Essentials.DeviceInfo.Idiom;

            // Device Type (Physical)
            public static Xamarin.Essentials.DeviceType Type => Xamarin.Essentials.DeviceInfo.DeviceType;

            public static bool IsiOS => Device.RuntimePlatform == Device.iOS;

            public static bool IsAndroid => Device.RuntimePlatform == Device.Android;

            public static bool IsUWP => Device.RuntimePlatform == Device.UWP;
        }
    }
}
