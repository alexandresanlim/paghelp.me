using pix_dynamic_payload_generator.net.Models;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;

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
        public Cob PixDynamicCob { get; set; }

        [LiteDB.BsonIgnore]
        public Payload Payload { get; set; }

        [LiteDB.BsonIgnore]
        public new PixPayloadCommand Commands { get; set; }
    }
}
