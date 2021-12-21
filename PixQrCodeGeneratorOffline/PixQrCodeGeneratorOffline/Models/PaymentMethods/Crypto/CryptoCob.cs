using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using PixQrCodeGeneratorOffline.Models.Validation.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto
{
    public class CryptoCob : CobBase
    {
        private readonly ICryptoCobViewerService _cryptoCobViewerService;

        private readonly ICryptoCobValidationService _cryptoCobValidationService;

        public CryptoCob()
        {
            _cryptoCobViewerService = DependencyService.Get<ICryptoCobViewerService>();
            _cryptoCobValidationService = DependencyService.Get<ICryptoCobValidationService>();
        }

        public CryptoCobViewer Viewer => _cryptoCobViewerService?.Create(this) ?? new CryptoCobViewer();

        public CryptoCobValidation Validation => _cryptoCobValidationService?.Create(this) ?? new CryptoCobValidation();
    }
}
