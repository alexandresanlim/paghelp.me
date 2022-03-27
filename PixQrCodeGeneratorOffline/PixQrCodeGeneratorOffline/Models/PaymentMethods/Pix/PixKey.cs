using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Viewer;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix
{
    public class PixKey : KeyBase
    {
        public PixKey()
        {
            Type = KeyType.Pix;
        }

        public string Name { get; set; }

        public string City { get; set; }

        public bool IsPin { get; set; }

        public FinancialInstitution FinancialInstitution { get; set; }

        [LiteDB.BsonIgnore]
        public PixKeyViewer Viewer { get; set; }

        [LiteDB.BsonIgnore]
        public PixPayload Payload { get; set; }

        [LiteDB.BsonIgnore]
        public PixKeyCommand Command { get; set; }
    }
}
