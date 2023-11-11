using PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Base;

namespace PixQrCodeGeneratorOffline.Models.Viewer
{
    public class PixKeyViewer : KeyViewerBase
    {
        public string NameAndCity { get; set; }

        public string NamePresentation { get; set; }

        public string BankAndKey { get; set; }

        public string Initial { get; set; }

        public char StartLetter { get; set; }
    }
}
