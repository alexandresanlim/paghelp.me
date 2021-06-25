using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IFinancialInstitutionService
    {
        List<FinancialInstitution> GetList();

        FinancialInstitution Create(FinancialInstitutionType financialInstitutionType, bool availablePremium = false);

        //IInstitution GetInstitution(FinancialInstitution financialInstitution);

        Institution GetInstitutionInstance(FinancialInstitution financialInstitution);
    }
}
