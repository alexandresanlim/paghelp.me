using PixQrCodeGeneratorOffline.Extention;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class MaterialColor
    {
        public MaterialColor()
        {
            TextPrimary = Color.FromHex("#212121");
            TextSecondary = Color.FromHex("#757575");
            BackgroundPage = Color.FromHex("#ecf0f1");
            Secondary = Color.FromHex("#50000000");
            TextOnSecondary = Color.FromHex("#ffffff");
        }

        public string Name { get; set; }

        public Color Primary { get; set; }

        public Color PrimaryLight { get; set; }

        public Color PrimaryDark { get; set; }

        public Color PrimaryTransparence => Primary.SetTransparence(0.5);

        public Color Secondary { get; set; }

        public Color SecondaryLight { get; set; }

        public Color SecondaryDark { get; set; }

        public Color TextOnPrimary { get; set; }

        public Color TextOnSecondary { get; set; }

        public Color BackgroundPage { get; set; }

        public Color White => Color.White;

        public Color Black => Color.Black;

        public Color TextPrimary { get; set; }

        public Color TextSecondary { get; set; }
    }
}
