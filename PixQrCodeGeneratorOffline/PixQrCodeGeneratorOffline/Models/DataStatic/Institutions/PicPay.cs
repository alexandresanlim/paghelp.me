using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class PicPay : InstitutionBase, IInstitution
    {
        public string Name => "PicPay";

        public FinancialInstitutionType Type => FinancialInstitutionType.PicPay;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "picpay",
            Primary = Color.FromHex("#23c45c"),
            PrimaryDark = Color.FromHex("#00922f"),
            PrimaryLight = Color.FromHex("#67f88b"),
            TextOnPrimary = Color.FromHex("#000000")
        };
    }
}
