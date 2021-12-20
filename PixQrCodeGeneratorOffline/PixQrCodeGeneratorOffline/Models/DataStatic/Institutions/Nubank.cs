using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Nubank : InstitutionBase, IInstitutionBank
    {
        public string Name => "Nubank";

        public FinancialInstitutionType Type => FinancialInstitutionType.Nubank;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "nubank",
            Primary = Color.FromHex("#8c04bc"),
            PrimaryDark = Color.FromHex("#58008b"),
            PrimaryLight = Color.FromHex("#c14aef"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
