using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class BancoOriginal : InstitutionBase, IInstitution
    {
        public string Name => "Banco Original";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancoOriginal;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bancooriginal",
            Primary = Color.FromHex("#3ba35c"),
            PrimaryDark = Color.FromHex("#007331"),
            PrimaryLight = Color.FromHex("#6fd58a"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
