using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Iti : InstitutionBase, IInstitution
    {
        public string Name => "Iti Itaú";

        public FinancialInstitutionType Type => FinancialInstitutionType.Iti;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "itiitau",
            Primary = Color.FromHex("#fb5493"),
            PrimaryDark = Color.FromHex("#c41066"),
            PrimaryLight = Color.FromHex("#ff89c3"),
            TextOnPrimary = Color.FromHex("#000000")
        };
    }
}
