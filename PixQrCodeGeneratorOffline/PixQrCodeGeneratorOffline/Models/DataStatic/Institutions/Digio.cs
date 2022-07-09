using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Digio : InstitutionBase, IInstitutionBank
    {
        public string Name => "Digio";

        public FinancialInstitutionType Type => FinancialInstitutionType.Digio;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "digio",
            Primary = Color.FromHex("#042c5c"),
            PrimaryDark = Color.FromHex("#000032"),
            PrimaryLight = Color.FromHex("#3d548a"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
