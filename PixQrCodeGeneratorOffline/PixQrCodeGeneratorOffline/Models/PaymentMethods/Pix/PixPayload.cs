using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix
{
    public class PixPayload : PayloadBase
    {
        private readonly IPixPayloadCommand _pixPayloadCommand;

        public PixPayload()
        {
            _pixPayloadCommand = DependencyService.Get<IPixPayloadCommand>();
            Type = PayloadType.Pix;
        }

        public PixKey PixKey { get; set; }

        public PixCob PixCob { get; set; }

        [LiteDB.BsonIgnore]
        public pix_payload_generator.net.Models.PayloadModels.Payload Payload { get; set; }

        [LiteDB.BsonIgnore]
        public PixPayloadCommand Commands => _pixPayloadCommand?.Create(this) ?? new PixPayloadCommand();
    }
}
