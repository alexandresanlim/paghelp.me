using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomEntry : Entry
    {
        public CustomEntry()
        {
            //BackgroundColor = Color.Red;
            TextColor = App.ThemeColors.TextPrimary;
            PlaceholderColor = App.ThemeColors.TextSecondary;
            ClearButtonVisibility = ClearButtonVisibility.WhileEditing;
            FontFamily = "FontPoppinsRegular";
            //FontSize = Device.GetNamedSize(NamedSize.Small, typeof(CustomEntry));
            //BackgroundColor = App.ThemeColors.TextOnPrimary;
            //PlaceholderColor = App.Style.SecondaryTextColor;

        }
    }
}
