using PixQrCodeGeneratorOffline.Models.Base;

namespace PixQrCodeGeneratorOffline.Models.Viewer.PaymentMethods.Base
{
    public class KeyViewerBase : NotifyObjectBase
    {
        public string KeyPresentation { get; set; }

        public string InstitutionPresentation { get; set; }

        public string InstitutionAndKey { get; set; }

        private bool _isHideValue;
        public bool IsHideValue
        {
            get => _isHideValue;
            set => SetProperty(ref _isHideValue, value);
        }
    }
}
