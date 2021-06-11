using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    class Bradesco : InstitutionBase, IInstitution
    {
        public string Name => "Bradesco";

        public FinancialInstitutionType Type => FinancialInstitutionType.Bradesco;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
