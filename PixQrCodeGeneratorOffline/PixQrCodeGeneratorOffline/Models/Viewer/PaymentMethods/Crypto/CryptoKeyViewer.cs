using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Crypto
{
    public class CryptoKeyViewer : KeyViewerBase
    {
        public string BankAndKey { get; set; }

        public string Initial { get; set; }
    }
}
