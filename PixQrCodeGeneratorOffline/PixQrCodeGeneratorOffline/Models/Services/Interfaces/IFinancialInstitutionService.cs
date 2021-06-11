using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IFinancialInstitutionService
    {
        List<FinancialInstitution> GetList();
    }
}
