
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
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            StartPackages(savedInstanceState);

            CurrentWindow = (this).Window;
            //LoadWindowColors();


            StartAndroidDependency();

            LoadApplication(new App());
        }

        private void StartPackages(Bundle savedInstanceState)
        {
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Acr.UserDialogs.UserDialogs.Init(this);
            MobileAds.Initialize(ApplicationContext);
            CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);
            //ZXing.Net.Mobile.Forms.Android.Platform.Init();
            //CrossCurrentActivity.Current.Init(this, savedInstanceState);
            //PlatformGestureEffect.Init();
            //Lottie.Forms.Droid.AnimationViewRenderer.Init();

            AppCenter.Start(App.Ids.AppCenter, typeof(Analytics), typeof(Crashes));
        }

        private void StartAndroidDependency()
        {
            DependencyService.Register<IStatusBar, StatusBarChanger>();
            DependencyService.Register<IPDVMode, PDVMode>();
        }

        //private void LoadWindowColors()
        //{
        //    //var color = (System.Drawing.Color)App.ThemeColors.Primary;

        //    //CurrentWindow.AddFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
        //    //CurrentWindow.AddFlags(Android.Views.WindowManagerFlags.TranslucentNavigation);

        //    if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
        //        return;

        //    CurrentWindow.SetStatusBarColor(Android.Graphics.Color.Black);
        //    CurrentWindow.SetNavigationBarColor(Android.Graphics.Color.Black);
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class StatusBarChanger : IStatusBar
        {
            public void SetColor()
            {
                SetStatusBarColor(App.ThemeColors.PrimaryDark);
            }

            public void SetStatusBarColor(System.Drawing.Color statusBar, System.Drawing.Color? navigationBar = null)
            {
                if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                    return;

                //CurrentWindow.AddFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                //CurrentWindow.AddFlags(Android.Views.WindowManagerFlags.TranslucentNavigation);
                //CurrentWindow.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
                CurrentWindow.SetStatusBarColor(statusBar.ToPlatformColor());


                //CurrentWindow.SetNavigationBarColor(navigationBar.ToPlatformColor());
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