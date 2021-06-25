using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Gerencianet : InstitutionBase, IInstitution
    {
        public string Name => "Gerencianet";

        public FinancialInstitutionType Type => FinancialInstitutionType.Gerencianet;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "gerencianet",
            Primary = Color.FromHex("#f4741c"),
            PrimaryDark = Color.FromHex("#ba4500"),
            PrimaryLight = Color.FromHex("#ffa54e"),
            TextOnPrimary = Color.FromHex("#000000")
        };
    }
}
