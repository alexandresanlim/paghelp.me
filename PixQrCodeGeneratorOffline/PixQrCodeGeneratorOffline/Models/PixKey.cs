using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
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
        private readonly IPixKeyViewerService _pixKeyViewerService;

        public PixKey()
        {
            _pixKeyViewerService = DependencyService.Get<IPixKeyViewerService>();
        }

        [LiteDB.BsonId]
        public int Id { get; set; }

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

        //public PixKeyType Type { get; set; }

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
        public PixKeyViewer Viewer => _pixKeyViewerService.Create(this);

        [LiteDB.BsonIgnore]
        public string ValuePresentation => "R$ " + Value;

        public void RaiseCob()
        {
            if (string.IsNullOrEmpty(Key))
                return;

            var value = Value?.Replace(".", "")?.Replace(",", ".") ?? "";

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

                var payload = cobranca?.ToPayload("PIXAPP" + Guid.NewGuid().ToString("N").Substring(0, 10), new Merchant(Name, City));

                Payload = payload?.GenerateStringToQrCode();
            });
        }

        public PixKeyType GetKeyType()
        {
            if (Key.IsEmail())
                return PixKeyType.Email;

            if (Key.IsCPF())
                return PixKeyType.CPF;

            if (Key.IsCNPJ())
                return PixKeyType.CNPJ;

            return PixKeyType.NotFound;
        }
    }

    public enum PixKeyType
    {
        NotFound = -1,
        Aleatoria,
        Celular,
        Email,
        CPF,
        CNPJ
    }
}
