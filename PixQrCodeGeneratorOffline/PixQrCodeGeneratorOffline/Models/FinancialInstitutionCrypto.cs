using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.DataStatic.InstitutionsCrypto.Base;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class FinancialInstitutionCrypto : FinancialInstitutionBase
    {
        private readonly IFinancialInstitutionCryptoService _financialInstitutionService;

        public FinancialInstitutionCrypto()
        {
            _financialInstitutionService = DependencyService.Get<IFinancialInstitutionCryptoService>();
        }

        public FinancialInstitutionCryptoType Type { get; set; }

        [LiteDB.BsonIgnore]
        public InstitutionCrypto Institution => _financialInstitutionService.GetInstitutionInstance(this) ?? new InstitutionCrypto();
    }

    public enum FinancialInstitutionCryptoType
    {
        Bitcoin,
        None
    }
}
