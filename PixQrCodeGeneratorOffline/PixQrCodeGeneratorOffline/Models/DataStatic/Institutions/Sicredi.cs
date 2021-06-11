using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class Sicredi : InstitutionBase, IInstitution
    {
        public string Name => "Sicredi";

        public FinancialInstitutionType Type => FinancialInstitutionType.Sicredi;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
