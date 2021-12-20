using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto
{
    public class Bitcoin : InstitutionBase, IInstitutionCrypto
    {
        public string Name => "Bitcoin";

        public FinancialInstitutionCryptoType Type => FinancialInstitutionCryptoType.Bitcoin;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bitcoin",
            Primary = Color.FromHex("#f39c12"),
            PrimaryDark = Color.FromHex("#bb6e00"),
            PrimaryLight = Color.FromHex("#ffcd4e"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
