using PixQrCodeGeneratorOffline.Models.DataStatic.Base;

namespace PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Base
{
    public class InstitutionCrypto : Institution
    {
        public FinancialInstitutionCryptoType Type { get; set; }
    }
}
