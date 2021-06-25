using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class BancoInter : InstitutionBase, IInstitution
    {
        public string Name => "Banco Inter";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancoInter;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bancointer",
            Primary = Color.FromHex("#fc7c04"),
            PrimaryDark = Color.FromHex("#c24d00"),
            PrimaryLight = Color.FromHex("#ffad44"),
            TextOnPrimary = Color.FromHex("#000000")
        };
    }
}
