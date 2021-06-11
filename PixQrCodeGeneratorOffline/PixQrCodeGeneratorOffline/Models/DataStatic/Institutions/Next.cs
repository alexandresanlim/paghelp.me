using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Next : InstitutionBase, IInstitution
    {
        public string Name => "Next";

        public FinancialInstitutionType Type => FinancialInstitutionType.Next;

        public MaterialColor MaterialColor => new MaterialColor()
        {
            Name = "next",
            Primary = Color.FromHex("#24fb64"),
            PrimaryDark = Color.FromHex("#00c632"),
            PrimaryLight = Color.FromHex("#75ff95"),
            TextOnPrimary = Color.FromHex("#000000")
        };
    }
}
