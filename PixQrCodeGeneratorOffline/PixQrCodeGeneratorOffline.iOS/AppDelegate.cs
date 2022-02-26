using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Google.MobileAds;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;

namespace PixQrCodeGeneratorOffline.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
   
            global::Xamarin.Forms.Forms.Init();

            MobileAds.SharedInstance.Start(CompletionHandler);
            //Lottie.Forms.iOS.Renderers.AnimationViewRenderer.Init();
            //ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            AppCenter.Start("c586d6bd-a614-4d5a-b786-8acd36a85fa6", typeof(Analytics), typeof(Crashes));

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void CompletionHandler(InitializationStatus status) { }
    }
}
