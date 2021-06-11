using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace PixQrCodeGeneratorOffline.Models.DataStatic.Institutions
{
    public class MercadoPago : InstitutionBase, IInstitution
    {
        public string Name => "Mercado Pago";

        public FinancialInstitutionType Type => FinancialInstitutionType.MercadoPago;

        public MaterialColor Color => _materialColorService.GetColorByFinancialInstitutionType(Type);
    }
}
