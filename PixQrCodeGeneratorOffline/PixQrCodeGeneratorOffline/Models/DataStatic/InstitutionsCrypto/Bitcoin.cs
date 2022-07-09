using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto
{
    public class Bitcoin : InstitutionBase, IInstitutionCrypto
    {
        public string Name => "Bitcoin";

        public string LinkToWallet => "bitcoin:";

        public FinancialInstitutionCryptoType Type => FinancialInstitutionCryptoType.Bitcoin;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bitcoin",
            Primary = Color.FromHex("#f69c3d"),
            PrimaryDark = Color.FromHex("#be6d02"),
            PrimaryLight = Color.FromHex("#ffcd6d"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };

        public string Code => "BTC";
    }
}
