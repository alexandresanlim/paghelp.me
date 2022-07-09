using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class Caixa : InstitutionBase, IInstitutionBank
    {
        public string Name => "Caixa";

        public FinancialInstitutionType Type => FinancialInstitutionType.Caixa;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "caixa",
            Primary = Color.FromHex("#0473bb"),
            PrimaryDark = Color.FromHex("#00488a"),
            PrimaryLight = Color.FromHex("#58a1ee"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
