using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class BancoBTGPactual : InstitutionBase, IInstitutionBank
    {
        public string Name => "BTG Pactual";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancoBTGPactual;

        public new string Icon => FontBancos.BtgPactual;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bancobtgpactual",
            Primary = Color.FromHex("#2596be"),
            PrimaryDark = Color.FromHex("#00688e"),
            PrimaryLight = Color.FromHex("#66c7f1"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
