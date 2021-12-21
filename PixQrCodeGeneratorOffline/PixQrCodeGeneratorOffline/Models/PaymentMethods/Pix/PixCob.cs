using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using PixQrCodeGeneratorOffline.Models.Validation;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix
{
    public class PixCob : CobBase
    {
        private readonly IPixCobViewerService _pixCobViewerService;

        private readonly IPixCobValidationService _pixCobValidationService;

        public PixCob()
        {
            _pixCobViewerService = DependencyService.Get<IPixCobViewerService>();
            _pixCobValidationService = DependencyService.Get<IPixCobValidationService>();
        }

        public PixCobViewer Viewer => _pixCobViewerService?.Create(this) ?? new PixCobViewer();

        public PixCobValidation Validation => _pixCobValidationService?.Create(this) ?? new PixCobValidation();
    }
}
