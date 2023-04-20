using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class BancoOriginal : InstitutionBase, IInstitutionBank
    {
        public string Name => "Banco Original";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancoOriginal;

        public new string Icon => FontBancos.Original;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bancooriginal",
            Primary = Color.FromHex("#3ba35c"),
            PrimaryDark = Color.FromHex("#007331"),
            PrimaryLight = Color.FromHex("#6fd58a"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
