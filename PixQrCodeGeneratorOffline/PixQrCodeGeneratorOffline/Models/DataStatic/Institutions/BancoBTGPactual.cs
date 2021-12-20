using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class BancoBTGPactual : InstitutionBase, IInstitutionBank
    {
        public string Name => "Banco BTG Pactual";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancoBTGPactual;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bancobtgpactual",
            Primary = Color.FromHex("#2596be"),
            PrimaryDark = Color.FromHex("#00688e"),
            PrimaryLight = Color.FromHex("#66c7f1"),
            TextOnPrimary = Color.FromHex("#000000")
        };
    }
}
