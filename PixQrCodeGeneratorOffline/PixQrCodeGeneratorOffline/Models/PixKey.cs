using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Style;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class PixKey : NotifyObjectBase
    {
        [LiteDB.BsonId]
        public int Id { get; set; }

        //public string Key { get; set; }

        private string _key;
        public string Key
        {
            set { SetProperty(ref _key, value); }
            get { return _key; }
        }

        public string Name { get; set; }

        public string City { get; set; }

        public MaterialColor Color { get; set; }

        public FinancialInstitution FinancialInstitution { get; set; }

        [LiteDB.BsonIgnore]
        private string _value;
        public string Value
        {
            set { SetProperty(ref _value, value); }
            get { return _value; }
        }

        [LiteDB.BsonIgnore]
        private string _description;
        public string Description
        {
            set { SetProperty(ref _description, value); }
            get { return _description; }
        }


        [LiteDB.BsonIgnore]
        private string _payload;
        public string Payload
        {
            set { SetProperty(ref _payload, value); }
            get { return _payload; }
        }

        [LiteDB.BsonIgnore]
        public string NameAndCity => (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(City)) ? Name + ", " + City : "";

        [LiteDB.BsonIgnore]
        public string NamePresentation => !string.IsNullOrEmpty(Name) ? Name : "";

        [LiteDB.BsonIgnore]
        public string KeyPresentation => !string.IsNullOrEmpty(Key) ? "Chave: " + Key : "";

        [LiteDB.BsonIgnore]
        public string InstitutionPresentation => !string.IsNullOrEmpty(FinancialInstitution?.Name) ? FinancialInstitution?.Name : "";

        //[LiteDB.BsonIgnore]
        //public string ValueUSString => !string.IsNullOrEmpty(Value) ? Value.Replace(",", ".") : "";

        //[LiteDB.BsonIgnore]
        //public decimal ValueUSCulture => !string.IsNullOrEmpty(Value) ? decimal.Parse(ValueUSString) : 0;

        [LiteDB.BsonIgnore]
        public string ValuePresentation => "R$ " + Value;

        public void RaiseCob()
        {
            if (string.IsNullOrEmpty(Key))
                return;

            var value = Value?.Replace(".", "")?.Replace(",",".") ?? "";

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                Cobranca cobranca = new Cobranca(_chave: Key)
                {
                    SolicitacaoPagador = Description,
                    Valor = new Valor
                    {
                        Original = value
                    }
                };

                var payload = cobranca?.ToPayload("PIXOFF" + Guid.NewGuid().ToString("N").Substring(0, 10), new Merchant(Name, City));

                Payload = payload?.GenerateStringToQrCode();
            });
        }
    }
}
