using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class SuperDigital : InstitutionBase, IInstitution
    {
        public string Name => "Super Digital";

        public FinancialInstitutionType Type => FinancialInstitutionType.Superdigital;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "superdigital",
            Primary = Color.FromHex("#6e0aa0"),
            PrimaryDark = Color.FromHex("#3b0070"),
            PrimaryLight = Color.FromHex("#a145d2"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
