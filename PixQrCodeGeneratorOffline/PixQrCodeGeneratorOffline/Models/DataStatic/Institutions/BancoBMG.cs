using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class BancoBMG : InstitutionBase, IInstitutionBank
    {
        public string Name => "Banco BMG";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancoBMG;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bancobmg",
            Primary = Color.FromHex("#fa7405"),
            PrimaryDark = Color.FromHex("#c04400"),
            PrimaryLight = Color.FromHex("#ffa543"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
