using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
