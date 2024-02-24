using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces;
using System;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto
{
    public class Theter : InstitutionBase, IInstitutionCrypto
    {
        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "theter",
            Primary = Color.FromHex("#2ea07b"),
            PrimaryDark = Color.FromHex("#00714f"),
            PrimaryLight = Color.FromHex("#66d2aa"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };

        public FinancialInstitutionCryptoType Type => FinancialInstitutionCryptoType.Theter;

        public string Name => "Theter (USD)";

        public string Code => "USDT";

        public string LinkToWallet => "theter:";
    }
}
