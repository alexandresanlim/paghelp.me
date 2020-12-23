using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomEditor : Editor
    {
        public CustomEditor()
        {
            TextColor = App.ThemeColors.TextPrimary;
            PlaceholderColor = App.ThemeColors.TextSecondary;
            //BackgroundColor = App.ThemeColors.TextOnPrimary;
            AutoSize = EditorAutoSizeOption.TextChanges;
            Keyboard = Keyboard.Create(KeyboardFlags.Suggestions | KeyboardFlags.CapitalizeCharacter);
            FontFamily = "FontPoppinsRegular";
            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(CustomEditor));
        }
    }
}
