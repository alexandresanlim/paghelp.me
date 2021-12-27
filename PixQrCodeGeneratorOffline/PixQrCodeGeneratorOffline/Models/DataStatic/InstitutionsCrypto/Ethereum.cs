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
            Primary = Color.FromHex("#f39c12"),
            PrimaryDark = Color.FromHex("#bb6e00"),
            PrimaryLight = Color.FromHex("#ffcd4e"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
