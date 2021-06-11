using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class BancoDoBrasil : InstitutionBase, IInstitution
    {
        public string Name => "Banco do Brasil";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancodoBrasil;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bancodobrasil",
            Primary = Color.FromHex("#fbf304"),
            PrimaryDark = Color.FromHex("#c4c100"),
            PrimaryLight = Color.FromHex("#ffff58"),
            TextOnPrimary = Color.FromHex("#000000")
        };
    }
}
