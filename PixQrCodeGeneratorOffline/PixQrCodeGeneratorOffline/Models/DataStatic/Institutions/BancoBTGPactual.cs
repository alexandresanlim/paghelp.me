using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class BancoBTGPactual : InstitutionBase, IInstitution
    {
        public string Name => "Banco BTG Pactual";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancoBTGPactual;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
