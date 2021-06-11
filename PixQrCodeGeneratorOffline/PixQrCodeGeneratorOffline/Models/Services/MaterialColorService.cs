using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class MaterialColorService : IMaterialColorService
    {
        public List<MaterialColor> GetNiceCombinationList()
        {
            return new List<MaterialColor>
            {
                new MaterialColor
                {
                    Name = "wine",
                    PrimaryDark = Color.FromHex("#001f2a"),
                    PrimaryLight = Color.FromHex("#2a4653"),
                    Primary = Color.FromHex("#264653"),
                    TextOnPrimary = Color.FromHex("#ffffff"),
                },
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

        public MaterialColor GetRandom()
        {
            return GetNiceCombinationList().PickRandom();
        }

        public MaterialColor GetColorByFinancialInstitutionType(FinancialInstitutionType financialInstitutionType)
        {
            switch (financialInstitutionType)
            {
                case FinancialInstitutionType.BancodoBrasil:
                    return new MaterialColor()
                    {
                        Name = "bancodobrasil",
                        Primary = Color.FromHex("#fbf304"),
                        PrimaryDark = Color.FromHex("#c4c100"),
                        PrimaryLight = Color.FromHex("#ffff58"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.BancoInter:
                    return new MaterialColor()
                    {
                        Name = "bancointer",
                        Primary = Color.FromHex("#fc7c04"),
                        PrimaryDark = Color.FromHex("#c24d00"),
                        PrimaryLight = Color.FromHex("#ffad44"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.BancoBMG:
                    return new MaterialColor()
                    {
                        Name = "bancobmg",
                        Primary = Color.FromHex("#fa7405"),
                        PrimaryDark = Color.FromHex("#c04400"),
                        PrimaryLight = Color.FromHex("#ffa543"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.BancoBTGPactual:
                    return new MaterialColor()
                    {
                        Name = "bancobtgpactual",
                        Primary = Color.FromHex("#2596be"),
                        PrimaryDark = Color.FromHex("#00688e"),
                        PrimaryLight = Color.FromHex("#66c7f1"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.BancoOriginal:
                    return new MaterialColor()
                    {
                        Name = "bancooriginal",
                        Primary = Color.FromHex("#3ba35c"),
                        PrimaryDark = Color.FromHex("#007331"),
                        PrimaryLight = Color.FromHex("#6fd58a"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.BancoPan:
                    return new MaterialColor()
                    {
                        Name = "bancopan",
                        Primary = Color.FromHex("#04acfb"),
                        PrimaryDark = Color.FromHex("#007dc8"),
                        PrimaryLight = Color.FromHex("#69deff"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.Bradesco:
                    return new MaterialColor()
                    {
                        Name = "bradesco",
                        Primary = Color.FromHex("#cc0c2c"),
                        PrimaryDark = Color.FromHex("#930004"),
                        PrimaryLight = Color.FromHex("#ff5355"),
                        TextOnPrimary = Color.FromHex("#ffffff"),
                    };
                case FinancialInstitutionType.BS2:
                    return new MaterialColor()
                    {
                        Name = "bs2",
                        Primary = Color.FromHex("#342cd4"),
                        PrimaryDark = Color.FromHex("#0000a1"),
                        PrimaryLight = Color.FromHex("#7659ff"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    };
                case FinancialInstitutionType.C6Bank:
                    return new MaterialColor()
                    {
                        Name = "c6bank",
                        Primary = Color.FromHex("#050505"),
                        PrimaryDark = Color.FromHex("#000000"),
                        PrimaryLight = Color.FromHex("#2f2f2f"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    };
                case FinancialInstitutionType.Caixa:
                    return new MaterialColor()
                    {
                        Name = "caixa",
                        Primary = Color.FromHex("#0473bb"),
                        PrimaryDark = Color.FromHex("#00488a"),
                        PrimaryLight = Color.FromHex("#58a1ee"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    };
                case FinancialInstitutionType.Digio:
                    return new MaterialColor()
                    {
                        Name = "digio",
                        Primary = Color.FromHex("#042c5c"),
                        PrimaryDark = Color.FromHex("#000032"),
                        PrimaryLight = Color.FromHex("#3d548a"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    };
                case FinancialInstitutionType.Gerencianet:
                    return new MaterialColor()
                    {
                        Name = "gerencianet",
                        Primary = Color.FromHex("#f4741c"),
                        PrimaryDark = Color.FromHex("#ba4500"),
                        PrimaryLight = Color.FromHex("#ffa54e"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.Itau:
                    return new MaterialColor()
                    {
                        Name = "itau",
                        Primary = Color.FromHex("#e97515"),
                        PrimaryDark = Color.FromHex("#b04700"),
                        PrimaryLight = Color.FromHex("#ffa549"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.MercadoPago:
                    return new MaterialColor()
                    {
                        Name = "mercadopago",
                        Primary = Color.FromHex("#05abf3"),
                        PrimaryDark = Color.FromHex("#007cc0"),
                        PrimaryLight = Color.FromHex("#67ddff"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.Neon:
                    return new MaterialColor()
                    {
                        Name = "neon",
                        Primary = Color.FromHex("#0bcbea"),
                        PrimaryDark = Color.FromHex("#009ab8"),
                        PrimaryLight = Color.FromHex("#69feff"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.Nubank:
                    return new MaterialColor()
                    {
                        Name = "nubank",
                        Primary = Color.FromHex("#8c04bc"),
                        PrimaryDark = Color.FromHex("#58008b"),
                        PrimaryLight = Color.FromHex("#c14aef"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    };
                case FinancialInstitutionType.Next:
                    return new MaterialColor()
                    {
                        Name = "next",
                        Primary = Color.FromHex("#24fb64"),
                        PrimaryDark = Color.FromHex("#00c632"),
                        PrimaryLight = Color.FromHex("#75ff95"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.PagBank:
                    return new MaterialColor()
                    {
                        Name = "pagbank",
                        Primary = Color.FromHex("#5cbb4c"),
                        PrimaryDark = Color.FromHex("#238a1c"),
                        PrimaryLight = Color.FromHex("#8fee7b"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.PicPay:
                    return new MaterialColor()
                    {
                        Name = "picpay",
                        Primary = Color.FromHex("#23c45c"),
                        PrimaryDark = Color.FromHex("#00922f"),
                        PrimaryLight = Color.FromHex("#67f88b"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.Safra:
                    return new MaterialColor()
                    {
                        Name = "safra",
                        Primary = Color.FromHex("#111925"),
                        PrimaryDark = Color.FromHex("#000000"),
                        PrimaryLight = Color.FromHex("#373f4d"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    };
                case FinancialInstitutionType.Santander:
                    return new MaterialColor()
                    {
                        Name = "santander",
                        Primary = Color.FromHex("#ec0404"),
                        PrimaryDark = Color.FromHex("#b00000"),
                        PrimaryLight = Color.FromHex("#ff5736"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    };
                case FinancialInstitutionType.Sicredi:
                    return new MaterialColor()
                    {
                        Name = "sicredi",
                        Primary = Color.FromHex("#66c434"),
                        PrimaryDark = Color.FromHex("#2e9300"),
                        PrimaryLight = Color.FromHex("#9af866"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.Superdigital:
                    return new MaterialColor()
                    {
                        Name = "superdigital",
                        Primary = Color.FromHex("#6e0aa0"),
                        PrimaryDark = Color.FromHex("#3b0070"),
                        PrimaryLight = Color.FromHex("#a145d2"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    };
                case FinancialInstitutionType.Iti:
                    return new MaterialColor()
                    {
                        Name = "itiitau",
                        Primary = Color.FromHex("#fb5493"),
                        PrimaryDark = Color.FromHex("#c41066"),
                        PrimaryLight = Color.FromHex("#ff89c3"),
                        TextOnPrimary = Color.FromHex("#000000")
                    };
                case FinancialInstitutionType.None:
                default:
                    return GetRandom();
            }
        }

        public MaterialColor GetLightColors()
        {
            return new MaterialColor
            {
                Primary = Color.FromHex("#ffffff"),
                PrimaryDark = Color.FromHex("#cccccc"),
                PrimaryLight = Color.FromHex("#ffffff"),
                Secondary = Color.FromHex("#34bcac"),
                TextOnPrimary = Color.FromHex("000000"),
                TextOnSecondary = Color.FromHex("000000")
            };
        }

        public MaterialColor GetDarkColors()
        {
            return new MaterialColor
            {
                Primary = Color.FromHex("#212121"),
                PrimaryDark = Color.FromHex("#000000"),
                PrimaryLight = Color.FromHex("#484848"),
                Secondary = Color.FromHex("#34bcac"),
                TextOnPrimary = Color.FromHex("ffffff"),
                TextOnSecondary = Color.FromHex("000000")
            };
        }

        public void SetOnCurrentResourceThemeColor(MaterialColor colors)
        {
            App.Current.Resources["primary"] = colors.Primary;
            App.Current.Resources["primaryLight"] = colors.PrimaryLight;
            App.Current.Resources["primaryDark"] = colors.PrimaryDark;

            App.Current.Resources["secondary"] = (colors?.Secondary == Color.FromRgba(0, 0, 0, 0)) ? Color.FromHex("#50000000") : colors.Secondary;
            App.Current.Resources["secondaryLight"] = colors.SecondaryLight;
            App.Current.Resources["secondaryDark"] = colors.SecondaryDark;


            App.Current.Resources["textOnPrimary"] = colors.TextOnPrimary;
            App.Current.Resources["textOnSecondary"] = (colors?.TextOnSecondary == Color.FromRgba(0, 0, 0, 0)) ? Color.White : colors.TextOnSecondary;
            App.Current.Resources["background_page"] = colors.BackgroundPage;

            App.Current.Resources["textPrimary"] = colors.TextPrimary;
            App.Current.Resources["textSecondary"] = colors.TextSecondary;
        }

        public MaterialColor GetByCurrentResourceThemeColor()
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
