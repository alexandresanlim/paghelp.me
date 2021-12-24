using PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Commands.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto
{
    public class CryptoPayload : PayloadBase
    {
        private readonly ICryptoPayloadCommand _pixPayloadCommand;

        public CryptoPayload()
        {
            _pixPayloadCommand = DependencyService.Get<ICryptoPayloadCommand>();
        }

        public CryptoKey CryptoKey { get; set; }

        public CryptoCob PixCob { get; set; }

        [LiteDB.BsonIgnore]
        public string Description { get; set; }

        [LiteDB.BsonIgnore]
        public string Amount { get; set; }

        [LiteDB.BsonIgnore]
        public CryptoPayloadCommand Commands => _pixPayloadCommand?.Create(this) ?? new CryptoPayloadCommand();
    }
}
