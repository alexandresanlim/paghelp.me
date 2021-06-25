using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Android.Gms.Ads;
using Plugin.Fingerprint;
using PixQrCodeGeneratorOffline.Services;

namespace PixQrCodeGeneratorOffline.Droid
{
    //[Activity(Label = "PixQrCodeGeneratorOffline", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    //[IntentFilter(new[] { Xamarin.Essentials.Platform.Intent.ActionAppAction }, Categories = new[] { Intent.CategoryDefault })]
    [Activity]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Window CurrentWindow { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Acr.UserDialogs.UserDialogs.Init(this);
            MobileAds.Initialize(ApplicationContext);
            CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);
            //ZXing.Net.Mobile.Forms.Android.Platform.Init();
            //CrossCurrentActivity.Current.Init(this, savedInstanceState);
            //PlatformGestureEffect.Init();
            //Lottie.Forms.Droid.AnimationViewRenderer.Init();
            AppCenter.Start("18439db5-b775-4a96-bb6f-6c4612d3daab", typeof(Analytics), typeof(Crashes));

            CurrentWindow = (this).Window;
            DependencyService.Register<IStatusBar, StatusBarChanger>();

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class StatusBarChanger : IStatusBar
        {
            public void SetByStyleListColor()
            {
                SetStatusBarColor(App.ThemeColors.Primary);
            }

            public void SetStatusBarColor(System.Drawing.Color color)
            {
                if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                    return;

                var window = CurrentWindow; //((MainActivity)Forms.Context).Window;
                window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
                window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                var androidColor = color.ToPlatformColor();

                window.SetStatusBarColor(androidColor);
            }
        }
    }
}