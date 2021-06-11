using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Digio : InstitutionBase, IInstitution
    {
        public string Name => "Digio";

        public FinancialInstitutionType Type => FinancialInstitutionType.Digio;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
