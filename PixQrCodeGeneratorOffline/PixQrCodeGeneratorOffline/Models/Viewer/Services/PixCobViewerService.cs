using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;

namespace PixQrCodeGeneratorOffline.Models.Viewer.Services
{
    public class PixCobViewerService : IPixCobViewerService
    {
        public PixCobViewer Create(PixCob pixCob)
        {
            return new PixCobViewer
            {
                ValueFormatter = GetValueFormatter(pixCob),
                ValuePresentation = GetValuePresentation(pixCob)
            };
        }

        private string GetValueFormatter(PixCob pixCob)
        {
            return pixCob.Value?.Replace(".", "")?.Replace(",", ".") ?? "";
        }

        private string GetValuePresentation(PixCob pixCob)
        {
            return "R$ " + pixCob.Value;
        }
    }
}
