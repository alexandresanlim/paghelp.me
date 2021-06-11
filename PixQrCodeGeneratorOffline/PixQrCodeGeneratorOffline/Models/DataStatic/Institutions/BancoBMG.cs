using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class BancoBMG : InstitutionBase, IInstitution
    {
        public string Name => "Banco BMG";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancoBMG;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
