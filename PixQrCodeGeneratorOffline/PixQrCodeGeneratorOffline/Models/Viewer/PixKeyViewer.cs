using PixQrCodeGeneratorOffline.Models.Base;

namespace PixQrCodeGeneratorOffline.Models.Viewer
{
    public class PixKeyViewer : NotifyObjectBase
    {
        public string NameAndCity { get; set; }

        public string NamePresentation { get; set; }

        public string KeyPresentation { get; set; }

        public string InstitutionPresentation { get; set; }

        public string InstitutionAndKey { get; set; }

        public string BankAndKey { get; set; }

        public string Initial { get; set; }

        private bool _isHideValue;
        public bool IsHideValue
        {
            get => _isHideValue;
            set => SetProperty(ref _isHideValue, value);
        }
    }
}
