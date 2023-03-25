using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Safra : InstitutionBase, IInstitutionBank
    {
        public string Name => "Safra";

        public FinancialInstitutionType Type => FinancialInstitutionType.Safra;

        public new string Icon => FontBancos.Safra;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "safra",
            Primary = Color.FromHex("#111925"),
            PrimaryDark = Color.FromHex("#000000"),
            PrimaryLight = Color.FromHex("#373f4d"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
