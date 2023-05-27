using PixQrCodeGeneratorOffline.Models.Base;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Base
{
    public class CobBase : NotifyObjectBase
    {
        private string _value;
        public string Value
        {
            set { SetProperty(ref _value, value); }
            get { return _value; }
        }

        private string _description;
        public string Description
        {
            set { SetProperty(ref _description, value); }
            get { return _description; }
        }

        private bool _isDynamic;
        public bool IsDynamic
        {
            set { SetProperty(ref _isDynamic, value); }
            get { return _isDynamic; }
        }
    }
}
