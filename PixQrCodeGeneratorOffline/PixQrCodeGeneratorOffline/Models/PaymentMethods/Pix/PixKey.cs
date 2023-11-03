using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Viewer;
using System.Collections.Generic;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix
{
    public class PixKeyGroup : List<PixKey>
    {
        public char Name { get; private set; }

        public PixKeyGroup(char name, List<PixKey> keys) : base(keys)
        {
            Name = name;
        }
    }

    public class PixKey : KeyBase
    {
        public PixKey()
        {
            Type = KeyType.Pix;
        }

        public string Name { get; set; }

        public string City { get; set; }

        public FinancialInstitution FinancialInstitution { get; set; }

        [LiteDB.BsonIgnore]
        public PixKeyViewer Viewer { get; set; }

        [LiteDB.BsonIgnore]
        public PixPayload Payload { get; set; }

        [LiteDB.BsonIgnore]
        public PixKeyCommand Command { get; set; }
    }
}
