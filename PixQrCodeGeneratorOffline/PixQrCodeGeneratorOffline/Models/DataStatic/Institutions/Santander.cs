using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Santander : InstitutionBase, IInstitution
    {
        public string Name => "Santander";

        public FinancialInstitutionType Type => FinancialInstitutionType.Santander;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
