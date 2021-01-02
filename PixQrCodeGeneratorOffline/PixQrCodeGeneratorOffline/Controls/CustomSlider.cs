using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomSlider : Slider
    {
        public CustomSlider()
        {
            ThumbColor = App.ThemeColors.PrimaryDark;
            MinimumTrackColor = App.ThemeColors.Primary;
            MaximumTrackColor = App.ThemeColors.TextSecondary;
        }
    }
}
