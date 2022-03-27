using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix
{
    public class PixCob : CobBase
    {
        private readonly IPixCobViewerService _pixCobViewerService;

        public PixCob()
        {
            _pixCobViewerService = DependencyService.Get<IPixCobViewerService>();
        }

        public PixCobViewer Viewer => _pixCobViewerService?.Create(this) ?? new PixCobViewer();
    }
}
