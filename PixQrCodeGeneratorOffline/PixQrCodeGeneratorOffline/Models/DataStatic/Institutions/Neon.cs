using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Neon : InstitutionBase, IInstitution
    {
        public string Name => "Neon";

        public FinancialInstitutionType Type => FinancialInstitutionType.Neon;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
