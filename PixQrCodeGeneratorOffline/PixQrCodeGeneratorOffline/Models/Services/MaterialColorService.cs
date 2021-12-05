using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class MaterialColorService : IMaterialColorService
    {
        public MaterialColor GetRandom()
        {
            return GetNiceCombinationList().PickRandom();
        }

        public List<MaterialColor> GetNiceCombinationList()
        {
            return new List<MaterialColor>
            {
                new MaterialColor
                {
                    Name = "wine",
                    PrimaryDark = Color.FromHex("#006e62"),
                    PrimaryLight = Color.FromHex("#459d8f"),
                    Primary = Color.FromHex("#2a9d8f"),
                    TextOnPrimary = Color.FromHex("#000000"),
                },
                new MaterialColor
                {
                    Name = "wine",
                    PrimaryDark = Color.FromHex("#b4943c"),
                    PrimaryLight = Color.FromHex("#e9c46a"),
                    Primary = Color.FromHex("#e9c46a"),
                    TextOnPrimary = Color.FromHex("#000000"),
                },
                new MaterialColor
                {
                    Name = "wine",
                    PrimaryDark = Color.FromHex("#be7334"),
                    PrimaryLight = Color.FromHex("#f4a261"),
                    Primary = Color.FromHex("#f4a261"),
                    TextOnPrimary = Color.FromHex("#000000"),
                },
                new MaterialColor
                {
                    Name = "wine",
                    PrimaryDark = Color.FromHex("#b04027"),
                    PrimaryLight = Color.FromHex("#e76f51"),
                    Primary = Color.FromHex("#e76f51"),
                    TextOnPrimary = Color.FromHex("#000000"),
                }
            };
        }

        private MaterialColor GetLightColors()
        {
            return new MaterialColor
            {
                Primary = Color.FromHex("#00796b"),
                PrimaryDark = Color.FromHex("#004c40"),
                PrimaryLight = Color.FromHex("#48a999"),
                //Secondary = Color.FromHex("#00796b"),
                TextOnPrimary = Color.FromHex("#ffffff"),
                //TextOnSecondary = Color.FromHex("#ffffff"),
                BackgroundPage = Color.FromHex("#ffffff"),
                ForegroundPage = Color.FromHex("#f0f1f5"),

                TextPrimary = Color.FromHex("#212121"),
                TextSecondary = Color.FromHex("#757575"),

                IsDarkOrLightTheme = true
            };
        }

        private MaterialColor GetDarkColors()
        {
            return new MaterialColor
            {
                Primary = Color.FromHex("#00796b"),
                PrimaryDark = Color.FromHex("#004c40"),
                PrimaryLight = Color.FromHex("#48a999"),
                //Secondary = Color.FromHex("#00796b"),
                TextOnPrimary = Color.FromHex("#ffffff"),
                //TextOnSecondary = Color.FromHex("#00000"),
                BackgroundPage = Color.FromHex("#000000"),
                ForegroundPage = Color.FromHex("#121212"),

                TextPrimary = Color.FromHex("#ffffff"),
                TextSecondary = Color.WhiteSmoke,

                IsDarkOrLightTheme = true
            };
        }

        public void SetOnCurrentResource(MaterialColor colors)
        {
            App.Current.Resources["primary"] = colors.Primary;
            App.Current.Resources["primaryLight"] = colors.PrimaryLight;
            App.Current.Resources["primaryDark"] = colors.PrimaryDark;

            App.Current.Resources["secondary"] = (colors?.Secondary == Color.FromRgba(0, 0, 0, 0)) ? Color.FromHex("#50000000") : colors.Secondary;
            App.Current.Resources["secondaryLight"] = colors.SecondaryLight;
            App.Current.Resources["secondaryDark"] = colors.SecondaryDark;


            App.Current.Resources["textOnPrimary"] = colors.TextOnPrimary;
            App.Current.Resources["textOnSecondary"] = (colors?.TextOnSecondary == Color.FromRgba(0, 0, 0, 0)) ? Color.White : colors.TextOnSecondary;

            if (colors.IsDarkOrLightTheme)
            {
                App.Current.Resources["background_page"] = colors.BackgroundPage;
                App.Current.Resources["foreground_page"] = colors.ForegroundPage;
                App.Current.Resources["textPrimary"] = colors.TextPrimary;
                App.Current.Resources["textSecondary"] = colors.TextSecondary;
            }
        }

        public MaterialColor GetOnCurrentResource()
        {
            var colorsResorce = new PixQrCodeGeneratorOffline.Style.Resources.Colors();

            return new MaterialColor
            {
                Primary = (Color)colorsResorce["primary"],
                PrimaryLight = (Color)colorsResorce["primaryLight"],
                PrimaryDark = (Color)colorsResorce["primaryDark"],

                Secondary = (Color)colorsResorce["secondary"],
                SecondaryLight = (Color)colorsResorce["secondaryLight"],
                SecondaryDark = (Color)colorsResorce["secondaryDark"],

                TextOnPrimary = (Color)colorsResorce["textOnPrimary"],
                TextOnSecondary = (Color)colorsResorce["textOnSecondary"],
                BackgroundPage = (Color)colorsResorce["background_page"],
                ForegroundPage = (Color)colorsResorce["foreground_page"],

                TextPrimary = (Color)colorsResorce["textPrimary"],
                TextSecondary = (Color)colorsResorce["textSecondary"],
            };
        }

        public MaterialColor GetByCurrentDeviceTheme()
        {
            //return (AppInfo.RequestedTheme == AppTheme.Light || AppInfo.RequestedTheme == AppTheme.Unspecified) ? GetLightColors() : GetDarkColors();

            return PixQrCodeGeneratorOffline.Services.Preference.ThemeIsDark ? GetDarkColors() : GetLightColors();
        }
    }
}
