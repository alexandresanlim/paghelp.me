using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Validation.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces.PaymentMethods.Crypto;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto
{
    public class CryptoKey : KeyBase
    {
        private readonly ICryptoKeyValidationService _cryptoKeyValidationService;

        public CryptoKey()
        {
            _cryptoKeyValidationService = DependencyService.Get<ICryptoKeyValidationService>();
            Type = KeyType.Crypto;
        }

        [LiteDB.BsonIgnore]
        public CryptoKeyValidation Validation => _cryptoKeyValidationService?.Create(this) ?? new CryptoKeyValidation();

        public FinancialInstitutionCrypto FinancialInstitution { get; set; }

        [LiteDB.BsonIgnore]
        public List<CryptoKeyAction> Actions => CryptoKeyAction.GetList(this);
    }
}
