using PixQrCodeGeneratorOffline.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models
{
    public class PixPayload : NotifyObjectBase
    {
        public pix_payload_generator.net.Models.PayloadModels.Payload Payload { get; set; }

        public PixKey PixKey { get; set; }

        public PixCob PixCob { get; set; }

        [LiteDB.BsonIgnore]
        private string _qrCode;
        public string QrCode
        {
            set { SetProperty(ref _qrCode, value); }
            get { return _qrCode; }
        }
    }
}
