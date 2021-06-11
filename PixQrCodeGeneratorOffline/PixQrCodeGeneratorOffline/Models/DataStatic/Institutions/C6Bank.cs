using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class C6Bank : InstitutionBase, IInstitution
    {
        public string Name => "C6 Bank";

        public FinancialInstitutionType Type => FinancialInstitutionType.C6Bank;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "c6bank",
            Primary = Color.FromHex("#050505"),
            PrimaryDark = Color.FromHex("#000000"),
            PrimaryLight = Color.FromHex("#2f2f2f"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
