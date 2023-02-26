using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace PixQrCodeGeneratorOffline.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash",
        MainLauncher = true,
        NoHistory = true,
        LaunchMode = LaunchMode.SingleTop,
        Exported = true,
        Label = App.AppName,
        Icon = "@mipmap/icon",
        HardwareAccelerated = true,
        WindowSoftInputMode = Android.Views.SoftInput.AdjustResize,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.Keyboard | ConfigChanges.ScreenSize | ConfigChanges.SmallestScreenSize | ConfigChanges.Locale | ConfigChanges.LayoutDirection | ConfigChanges.FontScale | ConfigChanges.ScreenLayout | ConfigChanges.Density | ConfigChanges.UiMode)]
    public class LoaderActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(LoaderActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            //Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            //Task startupWork = new Task(() => { StartApp(); });
            //startupWork.Start();
            StartApp();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        private void StartApp()
        {
            //Log.Debug(TAG, "Performing some startup work that takes a bit of time.");

            //await Task.Delay(12000);

            //Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}