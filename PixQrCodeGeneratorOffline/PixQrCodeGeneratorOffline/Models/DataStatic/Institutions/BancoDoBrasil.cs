using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class BancoDoBrasil : InstitutionBase, IInstitution
    {
        public string Name => "Banco do Brasil";

        public FinancialInstitutionType Type => FinancialInstitutionType.BancodoBrasil;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
