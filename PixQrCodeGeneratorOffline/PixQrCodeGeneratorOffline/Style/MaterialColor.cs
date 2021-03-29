using PixQrCodeGeneratorOffline.Extention;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Style
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

        //public Color TextOnLightThemePrimary => Black;

        //public Color TextOnLightThemeSecondary => TextOnLightThemePrimary.SetTransparence(0.8);

        //public Color TextOnDarkThemePrimary => White;

        //public Color TextOnDarkThemeSecondary => TextOnDarkThemePrimary.SetTransparence(0.8);

        public static List<MaterialColor> NiceCombinationList => new List<MaterialColor>
        {
            new MaterialColor
            {
                Name = "wine",
                PrimaryDark = Color.FromHex("#001f2a"),
                PrimaryLight = Color.FromHex("#2a4653"),
                Primary = Color.FromHex("#264653"),
                TextOnPrimary = Color.FromHex("#ffffff"),
                SecondaryDark = Color.FromHex("#3c0006"),
                Secondary = Color.FromHex("#6b1230"),
                TextOnSecondary = Color.FromHex("#FFFFFF")
            },
            new MaterialColor
            {
                Name = "wine",
                PrimaryDark = Color.FromHex("#006e62"),
                PrimaryLight = Color.FromHex("#459d8f"),
                Primary = Color.FromHex("#2a9d8f"),
                TextOnPrimary = Color.FromHex("#000000"),
                SecondaryDark = Color.FromHex("#3c0006"),
                Secondary = Color.FromHex("#6b1230"),
                TextOnSecondary = Color.FromHex("#FFFFFF")
            },
            new MaterialColor
            {
                Name = "wine",
                PrimaryDark = Color.FromHex("#b4943c"),
                PrimaryLight = Color.FromHex("#e9c46a"),
                Primary = Color.FromHex("#e9c46a"),
                TextOnPrimary = Color.FromHex("#000000"),
                SecondaryDark = Color.FromHex("#3c0006"),
                Secondary = Color.FromHex("#6b1230"),
                TextOnSecondary = Color.FromHex("#FFFFFF")
            },
            new MaterialColor
            {
                Name = "wine",
                PrimaryDark = Color.FromHex("#be7334"),
                PrimaryLight = Color.FromHex("#f4a261"),
                Primary = Color.FromHex("#f4a261"),
                TextOnPrimary = Color.FromHex("#000000"),
                SecondaryDark = Color.FromHex("#3c0006"),
                Secondary = Color.FromHex("#6b1230"),
                TextOnSecondary = Color.FromHex("#FFFFFF")
            },
            new MaterialColor
            {
                Name = "wine",
                PrimaryDark = Color.FromHex("#b04027"),
                PrimaryLight = Color.FromHex("#e76f51"),
                Primary = Color.FromHex("#e76f51"),
                TextOnPrimary = Color.FromHex("#000000"),
                SecondaryDark = Color.FromHex("#3c0006"),
                Secondary = Color.FromHex("#6b1230"),
                TextOnSecondary = Color.FromHex("#FFFFFF")
            }
        };

        public static MaterialColor GetRandom()
        {
            return NiceCombinationList.PickRandom();
        }

        public static void SetOnCurrentResourceThemeColor(MaterialColor colors)
        {
            App.Current.Resources["primary"] = colors.Primary;
            App.Current.Resources["primaryLight"] = colors.PrimaryLight;
            App.Current.Resources["primaryDark"] = colors.PrimaryDark;

            App.Current.Resources["secondary"] = (colors?.Secondary == Color.Transparent) ? Color.FromHex("#50000000") : colors.Secondary;
            App.Current.Resources["secondaryLight"] = colors.SecondaryLight;
            App.Current.Resources["secondaryDark"] = colors.SecondaryDark;


            App.Current.Resources["textOnPrimary"] = colors.TextOnPrimary;
            App.Current.Resources["textOnSecondary"] = (colors?.TextOnSecondary == Color.Transparent) ? Color.White : colors.TextOnSecondary;
            App.Current.Resources["background_page"] = colors.BackgroundPage;

            App.Current.Resources["textPrimary"] = colors.TextPrimary;
            App.Current.Resources["textSecondary"] = colors.TextSecondary;
        }

        public static MaterialColor GetByCurrentResourceThemeColor()
        {
            return new MaterialColor
            {
                Primary = (Color)App.Current.Resources["primary"],
                PrimaryLight = (Color)App.Current.Resources["primaryLight"],
                PrimaryDark = (Color)App.Current.Resources["primaryDark"],

                Secondary = (Color)App.Current.Resources["secondary"],
                SecondaryLight = (Color)App.Current.Resources["secondaryLight"],
                SecondaryDark = (Color)App.Current.Resources["secondaryDark"],

                TextOnPrimary = (Color)App.Current.Resources["textOnPrimary"],
                TextOnSecondary = (Color)App.Current.Resources["textOnSecondary"],
                BackgroundPage = (Color)App.Current.Resources["background_page"],

                TextPrimary = (Color)App.Current.Resources["textPrimary"],
                TextSecondary = (Color)App.Current.Resources["textSecondary"],
            };
        }
    }
}
