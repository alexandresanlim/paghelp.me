using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class PixPayload : NotifyObjectBase
    {
        private readonly IPixPayloadCommand _pixPayloadCommand;

        public PixPayload()
        {
            _pixPayloadCommand = DependencyService.Get<IPixPayloadCommand>();
        }

        [LiteDB.BsonId]
        public int Id { get; set; }

        public string Identity { get; set; }

        public PixKey PixKey { get; set; }

        public PixCob PixCob { get; set; }

        [LiteDB.BsonIgnore]
        public pix_payload_generator.net.Models.PayloadModels.Payload Payload { get; set; }

        [LiteDB.BsonIgnore]
        private string _qrCode;
        public string QrCode
        {
            set { SetProperty(ref _qrCode, value); }
            get { return _qrCode; }
        }

        [LiteDB.BsonIgnore]
        public PixPayloadCommand Commands => _pixPayloadCommand?.Create(this) ?? new PixPayloadCommand();
    }
}
