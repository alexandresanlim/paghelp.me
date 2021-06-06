using PixQrCodeGeneratorOffline.Style;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class FinancialInstitution
    {
        public string Name { get; set; }

        public string LogoUri { get; set; }

        public bool AvailablePremium { get; set; }

        [LiteDB.BsonIgnore]
        public MaterialColor Style { get; set; }
    }

    public static class FinancialInstitutionExtention
    {
        public static MaterialColor GetStyle(this FinancialInstitution financialInstitution)
        {
            return MaterialColor.GetRandom();
        }
    }
}
