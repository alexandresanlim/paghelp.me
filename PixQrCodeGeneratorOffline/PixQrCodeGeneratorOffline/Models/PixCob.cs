using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class PixCob : NotifyObjectBase
    {
        private string _value;
        public string Value
        {
            set { SetProperty(ref _value, value); }
            get { return _value; }
        }

        private string _description;
        public string Description
        {
            set { SetProperty(ref _description, value); }
            get { return _description; }
        }

        public string ValueFormatter => Value?.Replace(".", "")?.Replace(",", ".") ?? "";

        public string ValuePresentation => "R$ " + Value;
    }
}
