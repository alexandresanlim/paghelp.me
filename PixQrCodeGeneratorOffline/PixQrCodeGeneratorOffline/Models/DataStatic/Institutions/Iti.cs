using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Iti : InstitutionBase, IInstitution
    {
        public string Name => "Iti Itaú";

        public FinancialInstitutionType Type => FinancialInstitutionType.Iti;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
