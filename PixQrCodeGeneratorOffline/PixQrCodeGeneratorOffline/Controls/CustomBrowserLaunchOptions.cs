using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xamarin.Essentials;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomBrowserLaunchOptions : BrowserLaunchOptions
    {
        public CustomBrowserLaunchOptions()
        {
            //PreferredControlColor = Color.Transparent;
            PreferredToolbarColor = App.ThemeColors.PrimaryDark;
            LaunchMode = BrowserLaunchMode.SystemPreferred;
            TitleMode = BrowserTitleMode.Hide;
            //Flags = BrowserLaunchFlags.

        }
    }
}
