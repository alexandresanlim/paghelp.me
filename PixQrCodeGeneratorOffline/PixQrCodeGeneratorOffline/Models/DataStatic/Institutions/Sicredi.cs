using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Sicredi : InstitutionBase, IInstitutionBank
    {
        public string Name => "Sicredi";

        public FinancialInstitutionType Type => FinancialInstitutionType.Sicredi;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "sicredi",
            Primary = Color.FromHex("#66c434"),
            PrimaryDark = Color.FromHex("#2e9300"),
            PrimaryLight = Color.FromHex("#9af866"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
