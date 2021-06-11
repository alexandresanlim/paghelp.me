using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Style;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class FinancialInstitutionService : IFinancialInstitutionService
    {
        public List<FinancialInstitution> GetList()
        {
            return new List<FinancialInstitution>
            {
                new FinancialInstitution
                {
                    Name = "Banco do Brasil",
                    Style = new MaterialColor()
                    {
                        Name = "bancodobrasil",
                        Primary = Color.FromHex("#fbf304"),
                        PrimaryDark = Color.FromHex("#c4c100"),
                        PrimaryLight = Color.FromHex("#ffff58"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Banco Inter",
                    Style = new MaterialColor()
                    {
                        Name = "bancointer",
                        Primary = Color.FromHex("#fc7c04"),
                        PrimaryDark = Color.FromHex("#c24d00"),
                        PrimaryLight = Color.FromHex("#ffad44"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Banco BMG",
                    Style = new MaterialColor()
                    {
                        Name = "bancobmg",
                        Primary = Color.FromHex("#fa7405"),
                        PrimaryDark = Color.FromHex("#c04400"),
                        PrimaryLight = Color.FromHex("#ffa543"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Banco BTG Pactual",
                    Style = new MaterialColor()
                    {
                        Name = "bancobtgpactual",
                        Primary = Color.FromHex("#2596be"),
                        PrimaryDark = Color.FromHex("#00688e"),
                        PrimaryLight = Color.FromHex("#66c7f1"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Banco Original",
                    Style = new MaterialColor()
                    {
                        Name = "bancooriginal",
                        Primary = Color.FromHex("#3ba35c"),
                        PrimaryDark = Color.FromHex("#007331"),
                        PrimaryLight = Color.FromHex("#6fd58a"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                 new FinancialInstitution
                {
                    Name = "Banco Pan",
                    Style = new MaterialColor()
                    {
                        Name = "bancopan",
                        Primary = Color.FromHex("#04acfb"),
                        PrimaryDark = Color.FromHex("#007dc8"),
                        PrimaryLight = Color.FromHex("#69deff"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Bradesco",
                    Style = new MaterialColor()
                    {
                        Name = "bradesco",
                        Primary = Color.FromHex("#cc0c2c"),
                        PrimaryDark = Color.FromHex("#930004"),
                        PrimaryLight = Color.FromHex("#ff5355"),
                        TextOnPrimary = Color.FromHex("#ffffff"),
                    }
                },
                new FinancialInstitution
                {
                    Name = "BS2",
                    Style = new MaterialColor()
                    {
                        Name = "bs2",
                        Primary = Color.FromHex("#342cd4"),
                        PrimaryDark = Color.FromHex("#0000a1"),
                        PrimaryLight = Color.FromHex("#7659ff"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    }
                },
                new FinancialInstitution
                {
                    Name = "C6 Bank",
                    Style = new MaterialColor()
                    {
                        Name = "c6bank",
                        Primary = Color.FromHex("#050505"),
                        PrimaryDark = Color.FromHex("#000000"),
                        PrimaryLight = Color.FromHex("#2f2f2f"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Caixa",
                    Style = new MaterialColor()
                    {
                        Name = "caixa",
                        Primary = Color.FromHex("#0473bb"),
                        PrimaryDark = Color.FromHex("#00488a"),
                        PrimaryLight = Color.FromHex("#58a1ee"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Digio",
                    Style = new MaterialColor()
                    {
                        Name = "digio",
                        Primary = Color.FromHex("#042c5c"),
                        PrimaryDark = Color.FromHex("#000032"),
                        PrimaryLight = Color.FromHex("#3d548a"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Gerencianet",
                    Style = new MaterialColor()
                    {
                        Name = "gerencianet",
                        Primary = Color.FromHex("#f4741c"),
                        PrimaryDark = Color.FromHex("#ba4500"),
                        PrimaryLight = Color.FromHex("#ffa54e"),
                        TextOnPrimary = Color.FromHex("#000000")
                    },
                    AvailablePremium = true
                },
                new FinancialInstitution
                {
                    Name = "Itaú",
                    Style = new MaterialColor()
                    {
                        Name = "itau",
                        Primary = Color.FromHex("#e97515"),
                        PrimaryDark = Color.FromHex("#b04700"),
                        PrimaryLight = Color.FromHex("#ffa549"),
                        TextOnPrimary = Color.FromHex("#000000")
                    },
                    AvailablePremium = true
                },
                new FinancialInstitution
                {
                    Name = "Mercado Pago",
                    Style = new MaterialColor()
                    {
                        Name = "mercadopago",
                        Primary = Color.FromHex("#05abf3"),
                        PrimaryDark = Color.FromHex("#007cc0"),
                        PrimaryLight = Color.FromHex("#67ddff"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Neon",
                    Style = new MaterialColor()
                    {
                        Name = "neon",
                        Primary = Color.FromHex("#0bcbea"),
                        PrimaryDark = Color.FromHex("#009ab8"),
                        PrimaryLight = Color.FromHex("#69feff"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Nubank",
                    Style = new MaterialColor()
                    {
                        Name = "nubank",
                        Primary = Color.FromHex("#8c04bc"),
                        PrimaryDark = Color.FromHex("#58008b"),
                        PrimaryLight = Color.FromHex("#c14aef"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    }
                },
                 new FinancialInstitution
                {
                    Name = "Next",
                    Style = new MaterialColor()
                    {
                        Name = "next",
                        Primary = Color.FromHex("#24fb64"),
                        PrimaryDark = Color.FromHex("#00c632"),
                        PrimaryLight = Color.FromHex("#75ff95"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                 new FinancialInstitution
                {
                    Name = "PagBank (PagSeguro)",
                    Style = new MaterialColor()
                    {
                        Name = "pagbank",
                        Primary = Color.FromHex("#5cbb4c"),
                        PrimaryDark = Color.FromHex("#238a1c"),
                        PrimaryLight = Color.FromHex("#8fee7b"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                new FinancialInstitution
                {
                    Name = "PicPay",
                    Style = new MaterialColor()
                    {
                        Name = "picpay",
                        Primary = Color.FromHex("#23c45c"),
                        PrimaryDark = Color.FromHex("#00922f"),
                        PrimaryLight = Color.FromHex("#67f88b"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                 new FinancialInstitution
                {
                    Name = "Safra",
                    Style = new MaterialColor()
                    {
                        Name = "safra",
                        Primary = Color.FromHex("#111925"),
                        PrimaryDark = Color.FromHex("#000000"),
                        PrimaryLight = Color.FromHex("#373f4d"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Santander",
                    Style = new MaterialColor()
                    {
                        Name = "santander",
                        Primary = Color.FromHex("#ec0404"),
                        PrimaryDark = Color.FromHex("#b00000"),
                        PrimaryLight = Color.FromHex("#ff5736"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Sicredi",
                    Style = new MaterialColor()
                    {
                        Name = "sicredi",
                        Primary = Color.FromHex("#66c434"),
                        PrimaryDark = Color.FromHex("#2e9300"),
                        PrimaryLight = Color.FromHex("#9af866"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Superdigital",
                    Style = new MaterialColor()
                    {
                        Name = "superdigital",
                        Primary = Color.FromHex("#6e0aa0"),
                        PrimaryDark = Color.FromHex("#3b0070"),
                        PrimaryLight = Color.FromHex("#a145d2"),
                        TextOnPrimary = Color.FromHex("#ffffff")
                    }
                },
                new FinancialInstitution
                {
                    Name = "Iti Itaú",
                    Style = new MaterialColor()
                    {
                        Name = "itiitau",
                        Primary = Color.FromHex("#fb5493"),
                        PrimaryDark = Color.FromHex("#c41066"),
                        PrimaryLight = Color.FromHex("#ff89c3"),
                        TextOnPrimary = Color.FromHex("#000000")
                    }
                },
            };
        }
    }
}
