using PixQrCodeGeneratorOffline.Models.Base;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Base
{
    public class PayloadBase : NotifyObjectBase
    {
        [LiteDB.BsonId]
        public int Id { get; set; }

        public string Identity { get; set; }

        [LiteDB.BsonIgnore]
        private string _qrCode;
        public string QrCode
        {
            set { SetProperty(ref _qrCode, value); }
            get { return _qrCode; }
        }
    }
}
