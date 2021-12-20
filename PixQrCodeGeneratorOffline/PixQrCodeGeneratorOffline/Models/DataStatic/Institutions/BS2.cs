using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class BS2 : InstitutionBase, IInstitutionBank
    {
        public string Name => "BS2";

        public FinancialInstitutionType Type => FinancialInstitutionType.BS2;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bs2",
            Primary = Color.FromHex("#342cd4"),
            PrimaryDark = Color.FromHex("#0000a1"),
            PrimaryLight = Color.FromHex("#7659ff"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
