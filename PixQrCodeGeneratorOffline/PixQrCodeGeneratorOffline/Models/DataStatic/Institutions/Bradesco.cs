using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class Bradesco : InstitutionBase, IInstitution
    {
        public string Name => "Bradesco";

        public FinancialInstitutionType Type => FinancialInstitutionType.Bradesco;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bradesco",
            Primary = Color.FromHex("#cc0c2c"),
            PrimaryDark = Color.FromHex("#930004"),
            PrimaryLight = Color.FromHex("#ff5355"),
            TextOnPrimary = Color.FromHex("#ffffff"),
        };
    }
}
