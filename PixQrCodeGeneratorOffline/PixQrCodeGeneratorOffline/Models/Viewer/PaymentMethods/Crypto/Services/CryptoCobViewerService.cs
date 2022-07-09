using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto.Services
{
    public class CryptoCobViewerService : ICryptoCobViewerService
    {
        public CryptoCobViewer Create(CryptoCob pixCob)
        {
            return new CryptoCobViewer
            {
                ValueFormatter = GetValueFormatter(pixCob),
                ValuePresentation = GetValuePresentation(pixCob)
            };
        }

        private string GetValueFormatter(CryptoCob pixCob)
        {
            return pixCob.Value?.Replace(".", "")?.Replace(",", ".") ?? "";
        }

        private string GetValuePresentation(CryptoCob pixCob)
        {
            return "R$ " + pixCob.Value;
        }
    }
}
