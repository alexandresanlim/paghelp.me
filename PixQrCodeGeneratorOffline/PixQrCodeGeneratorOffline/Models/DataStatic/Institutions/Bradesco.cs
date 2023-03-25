using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class Bradesco : InstitutionBase, IInstitutionBank
    {
        public string Name => "Bradesco";

        public FinancialInstitutionType Type => FinancialInstitutionType.Bradesco;

        public new string Icon => FontBancos.Bradesco;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bradesco",
            Primary = Color.FromHex("#cc0c2c"),
            PrimaryDark = Color.FromHex("#930004"),
            PrimaryLight = Color.FromHex("#ff5355"),
            TextOnPrimary = Color.FromHex("#ffffff"),
        };
    }
}
