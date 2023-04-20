using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Neon : InstitutionBase, IInstitutionBank
    {
        public string Name => "Neon";

        public FinancialInstitutionType Type => FinancialInstitutionType.Neon;

        public new string Icon => FontBancos.Neon;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "neon",
            Primary = Color.FromHex("#0bcbea"),
            PrimaryDark = Color.FromHex("#009ab8"),
            PrimaryLight = Color.FromHex("#69feff"),
            TextOnPrimary = Color.FromHex("#000000")
        };
    }
}
