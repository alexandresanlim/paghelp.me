using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Validation;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix
{
    public class PixKey : KeyBase
    {
        private readonly IPixKeyViewerService _pixKeyViewerService;

        private readonly IPixPayloadService _pixPayloadService;

        private readonly IPixKeyCommand _pixKeyCommand;

        private readonly IPixKeyValidationService _pixKeyValidationService;

        public PixKey()
        {
            _pixKeyViewerService = DependencyService.Get<IPixKeyViewerService>();
            _pixPayloadService = DependencyService.Get<IPixPayloadService>();
            _pixKeyCommand = DependencyService.Get<IPixKeyCommand>();
            _pixKeyValidationService = DependencyService.Get<IPixKeyValidationService>();
            Type = KeyType.Pix;
        }

        public string Name { get; set; }

        public string City { get; set; }

        public FinancialInstitution FinancialInstitution { get; set; }

        [LiteDB.BsonIgnore]
        public PixKeyViewer Viewer { get; set; }

        [LiteDB.BsonIgnore]
        public PixPayload Payload { get; set; }

        [LiteDB.BsonIgnore]
        public PixKeyCommand Command { get; set; }

        [LiteDB.BsonIgnore]
        public PixKeyValidation Validation => _pixKeyValidationService?.Create(this) ?? new PixKeyValidation();
    }
}
