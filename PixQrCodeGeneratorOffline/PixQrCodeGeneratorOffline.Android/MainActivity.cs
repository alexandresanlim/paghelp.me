
using Android.App;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using Plugin.Fingerprint;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Droid
{
    //[Activity(Label = "PixQrCodeGeneratorOffline", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    //[IntentFilter(new[] { Xamarin.Essentials.Platform.Intent.ActionAppAction }, Categories = new[] { Intent.Categories.Add })]
    [Activity]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static Window CurrentWindow { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartPackages(savedInstanceState);
            CurrentWindow = (this).Window;
            StartAndroidDependency();
            LoadApplication(new App());
        }

        private void StartPackages(Bundle savedInstanceState)
        {
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Acr.UserDialogs.UserDialogs.Init(this);
            MobileAds.Initialize(ApplicationContext);
            CrossFingerprint.SetCurrentActivityResolver(() => Platform.CurrentActivity);
            Rg.Plugins.Popup.Popup.Init(this);
            //ConfigureWindow();
            AppCenter.Start(App.Ids.AppCenter, typeof(Analytics), typeof(Crashes));
        }

        //private void ConfigureWindow()
        //{
        //    Window.SetSoftInputMode(SoftInput.AdjustResize);
        //}

        private void StartAndroidDependency()
        {
            DependencyService.Register<IStatusBar, StatusBarChanger>();
            DependencyService.Register<IPDVMode, PDVMode>();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        public class StatusBarChanger : IStatusBar
        {
            public void SetColor()
            {
                SetStatusBarColor(App.ThemeColors.PrimaryDark, Color.Transparent);
            }

            public void SetStatusBarColor(System.Drawing.Color statusBar, System.Drawing.Color? navigationBar = null)
            {
                if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                    return;

                //CurrentWindow.AddFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                //CurrentWindow.AddFlags(Android.Views.WindowManagerFlags.TranslucentNavigation);
                //CurrentWindow.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                CurrentWindow.SetStatusBarColor(statusBar.ToPlatformColor());

                //if (navigationBar.HasValue)
                //    CurrentWindow.SetNavigationBarColor(navigationBar.Value.ToPlatformColor());

                CurrentWindow.SetNavigationBarColor(statusBar.ToPlatformColor());
            }
        }

        public class PDVMode : IPDVMode
        {
            public void SetPDVMode()
            {
                if (Preference.IsPDVMode)
                {
                    CurrentWindow.AddFlags(WindowManagerFlags.Fullscreen);
                    CurrentWindow.AddFlags(WindowManagerFlags.KeepScreenOn);
                }
            }
        }
    }
}