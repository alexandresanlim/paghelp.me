using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto
{
    public class BinanceCoin : InstitutionBase, IInstitutionCrypto
    {
        public FinancialInstitutionCryptoType Type => FinancialInstitutionCryptoType.Binancecoin;

        public string Name => "Binance Coin";

        public string Code => "BNB";

        public string LinkToWallet => "binance:";

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "binance",
            Primary = Color.FromHex("#f39c12"),
            PrimaryDark = Color.FromHex("#bb6e00"),
            PrimaryLight = Color.FromHex("#ffcd4e"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
