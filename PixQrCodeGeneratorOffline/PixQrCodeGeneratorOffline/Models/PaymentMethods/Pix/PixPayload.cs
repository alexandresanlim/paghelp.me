using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix
{
    public class PixPayload : PayloadBase
    {
        public PixPayload()
        {
            Type = PayloadType.Pix;
        }

        public PixKey PixKey { get; set; }

        public PixCob PixCob { get; set; }

        [LiteDB.BsonIgnore]
        public pix_payload_generator.net.Models.PayloadModels.Payload Payload { get; set; }

        [LiteDB.BsonIgnore]
        public PixPayloadCommand Commands { get; set; }
    }
}
