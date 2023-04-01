using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Itau : InstitutionBase, IInstitutionBank
    {
        public string Name => "Itaú";

        public FinancialInstitutionType Type => FinancialInstitutionType.Itau;

        public new string Icon => FontBancos.Itau;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "itau",
            Primary = Color.FromHex("#e97515"),
            PrimaryDark = Color.FromHex("#b04700"),
            PrimaryLight = Color.FromHex("#ffa549"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
