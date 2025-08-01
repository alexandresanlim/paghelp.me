﻿using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline
{
    public partial class App
    {
        public static IMaterialColorService MaterialColorService => DependencyService.Get<IMaterialColorService>();

        public static IStatusBar StatusBarService => DependencyService.Get<IStatusBar>();

        public static void LoadTheme()
        {
            var theme = MaterialColorService.GetByCurrentDeviceTheme();

            MaterialColorService.SetOnCurrentResource(theme);

            StatusBarService?.SetColor();
        }

        public static MaterialColor ThemeColors => MaterialColorService.GetOnCurrentResource();

        public class DeviceInfo
        {
            public static string Manufacturer => Xamarin.Essentials.DeviceInfo.Manufacturer;

            public static string Model => Xamarin.Essentials.DeviceInfo.Model;

            public static string ManufacturerAndModel => Manufacturer + " " + Model;

            public static string DeviceName => Xamarin.Essentials.DeviceInfo.Name;

            public static string Version => Xamarin.Essentials.DeviceInfo.VersionString;

            public static Xamarin.Essentials.DevicePlatform Platform => Xamarin.Essentials.DeviceInfo.Platform;

            public static string PlatformAndVersion => Platform.ToString() + " " + Version;

            public static Xamarin.Essentials.DeviceIdiom Idiom => Xamarin.Essentials.DeviceInfo.Idiom;

            public static Xamarin.Essentials.DeviceType Type => Xamarin.Essentials.DeviceInfo.DeviceType;

            public static bool IsiOS => Device.RuntimePlatform == Device.iOS;

            public static bool IsAndroid => Device.RuntimePlatform == Device.Android;

            public static bool IsUWP => Device.RuntimePlatform == Device.UWP;
        }

        public class Resorces
        {
            public static Style.Resources.ColorsResource Colors => new Style.Resources.ColorsResource();
        }
    }
}
