using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
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
                ForegroundPage = Color.FromHex("#ecf0f1"),

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
                ForegroundPage = (Color)App.Current.Resources["foreground_page"],

                TextPrimary = (Color)App.Current.Resources["textPrimary"],
                TextSecondary = (Color)App.Current.Resources["textSecondary"],
            };
        }

        public MaterialColor GetByCurrentDeviceTheme()
        {
            return (AppInfo.RequestedTheme == AppTheme.Light || AppInfo.RequestedTheme == AppTheme.Unspecified) ? GetLightColors() : GetDarkColors();
        }
    }
}
