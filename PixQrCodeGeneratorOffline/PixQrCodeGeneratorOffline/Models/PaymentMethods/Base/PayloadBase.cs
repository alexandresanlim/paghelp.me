using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Base
{
    public class PayloadBase : NotifyObjectBase
    {
        private readonly IPayloadCommandBase _payloadCommandBase;

        public PayloadBase()
        {
            _payloadCommandBase = DependencyService.Get<IPayloadCommandBase>();
        }

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

        public PayloadType Type { get; set; }

        [LiteDB.BsonIgnore]
        public PayloadCommandBase Commands => _payloadCommandBase?.Create(this) ?? new PayloadCommandBase();
    }

    public enum PayloadType
    {
        Pix,
        Crypto
    }
}
