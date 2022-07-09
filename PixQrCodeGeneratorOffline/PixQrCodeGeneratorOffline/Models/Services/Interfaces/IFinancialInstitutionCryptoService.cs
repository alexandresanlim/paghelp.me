using PixQrCodeGeneratorOffline.Models.DataStatic.Institutions.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Base;
using System.Collections.Generic;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IFinancialInstitutionCryptoService
    {
        List<FinancialInstitutionCrypto> GetList();

        FinancialInstitutionCrypto Create(FinancialInstitutionCryptoType financialInstitutionType);

        InstitutionCrypto GetInstitutionInstance(FinancialInstitutionCrypto financialInstitution);
    }
}
