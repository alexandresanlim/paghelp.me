using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto
{
    public class Ethereum : InstitutionBase, IInstitutionCrypto
    {
        public FinancialInstitutionCryptoType Type => FinancialInstitutionCryptoType.Ethereum;

        public string Name => "Ethereum";

        public string Code => "ETH";

        public string LinkToWallet => "ethereum:";

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "ethereum",
            Primary = Color.FromHex("#497493"),
            PrimaryDark = Color.FromHex("#174965"),
            PrimaryLight = Color.FromHex("#79a3c4"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
