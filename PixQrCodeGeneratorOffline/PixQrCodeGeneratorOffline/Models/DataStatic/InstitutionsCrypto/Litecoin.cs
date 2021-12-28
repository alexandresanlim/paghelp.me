using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces;
using System;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto
{
    public class Litecoin : InstitutionBase, IInstitutionCrypto
    {
        public FinancialInstitutionCryptoType Type => FinancialInstitutionCryptoType.Litecoin;

        public string Name => "Litecoin";

        public string Code => "LTC";

        public string LinkToWallet => "litecoin";

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "litecoin",
            Primary = Color.FromHex("#949494"),
            PrimaryDark = Color.FromHex("#666666"),
            PrimaryLight = Color.FromHex("#c4c4c4"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
