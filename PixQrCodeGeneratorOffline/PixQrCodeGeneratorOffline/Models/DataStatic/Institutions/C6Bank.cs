using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class C6Bank : InstitutionBase, IInstitutionBank
    {
        public string Name => "C6 Bank";

        public FinancialInstitutionType Type => FinancialInstitutionType.C6Bank;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "c6bank",
            Primary = Color.FromHex("#7a7a7a"),
            PrimaryDark = Color.FromHex("#4e4e4e"),
            PrimaryLight = Color.FromHex("#a9a9a9"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
