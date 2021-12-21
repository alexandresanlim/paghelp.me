using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.Validation.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto
{
    public class CryptoKey : KeyBase
    {
        private readonly ICryptoKeyViewerService _cryptoKeyViewerService;

        private readonly ICryptoKeyValidationService _cryptoKeyValidationService;

        private readonly ICryptoPayloadService _cryptoPayloadService;

        private readonly ICryptoKeyCommand _pixKeyCommand;

        public CryptoKey()
        {
            _cryptoKeyValidationService = DependencyService.Get<ICryptoKeyValidationService>();
            _cryptoPayloadService = DependencyService.Get<ICryptoPayloadService>();
            _pixKeyCommand = DependencyService.Get<ICryptoKeyCommand>();
            _cryptoKeyViewerService = DependencyService.Get<ICryptoKeyViewerService>();
            Type = KeyType.Crypto;
        }

        public FinancialInstitutionCrypto FinancialInstitution { get; set; }

        [LiteDB.BsonIgnore]
        public CryptoKeyValidation Validation => _cryptoKeyValidationService?.Create(this) ?? new CryptoKeyValidation();

        [LiteDB.BsonIgnore]
        public List<CryptoKeyAction> Actions => CryptoKeyAction.GetList(this);

        [LiteDB.BsonIgnore]
        public CryptoPayload Payload => _cryptoPayloadService?.Create(this) ?? new CryptoPayload();

        [LiteDB.BsonIgnore]
        public CryptoKeyCommand Command => _pixKeyCommand?.Create(this) ?? new CryptoKeyCommand();

        [LiteDB.BsonIgnore]
        public CryptoKeyViewer Viewer => _cryptoKeyViewerService?.Create(this) ?? new CryptoKeyViewer();
    }
}
