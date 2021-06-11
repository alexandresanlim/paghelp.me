using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class NaoInformado : InstitutionBase, IInstitution
    {
        public string Name => "Não Informado";

        public FinancialInstitutionType Type => FinancialInstitutionType.None;

        public MaterialColor MaterialColor => _materialColorService.GetRandom();
    }
}
