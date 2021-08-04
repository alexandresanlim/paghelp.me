using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class BancoPan : InstitutionBase, IInstitution
    {
        public string Name => "Banco Pan"; 
        
        public FinancialInstitutionType Type => FinancialInstitutionType.BancoPan;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "bancopan",
            Primary = Color.FromHex("#04acfb"),
            PrimaryDark = Color.FromHex("#007dc8"),
            PrimaryLight = Color.FromHex("#69deff"),
            TextOnPrimary = Color.FromHex("#ffffff")
        };
    }
}
