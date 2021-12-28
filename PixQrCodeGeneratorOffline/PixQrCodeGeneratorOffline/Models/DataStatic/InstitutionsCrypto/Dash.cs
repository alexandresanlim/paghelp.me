using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces;
using System;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto
{
    public class Dash : InstitutionBase, IInstitutionCrypto
    {
        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "dash",
            Primary = Color.FromHex("#1376b5"),
            PrimaryDark = Color.FromHex("#004b85"),
            PrimaryLight = Color.FromHex("#5ba4e8"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };

        public FinancialInstitutionCryptoType Type => FinancialInstitutionCryptoType.Dash;

        public string Name => "Dash";

        public string Code => "DASH";

        public string LinkToWallet => "dash:";
    }
}
