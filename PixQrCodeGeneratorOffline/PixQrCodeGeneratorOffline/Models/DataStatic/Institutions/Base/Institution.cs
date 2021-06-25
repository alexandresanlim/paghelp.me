using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base
{
    public class Institution
    {
        public FinancialInstitutionType Type { get; set; }

        public string Name { get; set; }

        public MaterialColor MaterialColor { get; set; }
    }
}
