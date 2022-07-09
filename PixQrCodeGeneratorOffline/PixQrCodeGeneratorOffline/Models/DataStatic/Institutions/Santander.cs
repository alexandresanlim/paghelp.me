using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Santander : InstitutionBase, IInstitutionBank
    {
        public string Name => "Santander";

        public FinancialInstitutionType Type => FinancialInstitutionType.Santander;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "santander",
            Primary = Color.FromHex("#ec0404"),
            PrimaryDark = Color.FromHex("#b00000"),
            PrimaryLight = Color.FromHex("#ff5736"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
