using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class BancoInter : InstitutionBase, IInstitutionBank
    {
        public string Name => "Banco Inter";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancoInter;

        public new string Icon => FontBancos.Inter;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bancointer",
            Primary = Color.FromHex("#fc7c04"),
            PrimaryDark = Color.FromHex("#c24d00"),
            PrimaryLight = Color.FromHex("#ffad44"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
