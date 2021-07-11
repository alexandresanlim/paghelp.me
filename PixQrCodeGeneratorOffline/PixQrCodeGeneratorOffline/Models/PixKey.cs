using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Base;
using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Validation;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
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

        private readonly IPixPayloadService _pixPayloadService;

        private readonly IPixKeyCommand _pixKeyCommand;

        private readonly IPixKeyValidationService _pixKeyValidationService;

        public PixKey()
        {
            _pixKeyViewerService = DependencyService.Get<IPixKeyViewerService>();
            _pixPayloadService = DependencyService.Get<IPixPayloadService>();
            _pixKeyCommand = DependencyService.Get<IPixKeyCommand>();
            _pixKeyValidationService = DependencyService.Get<IPixKeyValidationService>();
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

        public bool IsFavorite { get; set; }

        public bool IsContact { get; set; }

        public FinancialInstitution FinancialInstitution { get; set; }

        //public PixKeyType Type { get; set; }

        [LiteDB.BsonIgnore]
        public PixKeyViewer Viewer => _pixKeyViewerService?.Create(this) ?? new PixKeyViewer();

        [LiteDB.BsonIgnore]
        public PixKeyValidation Validation => _pixKeyValidationService?.Create(this) ?? new PixKeyValidation();

        [LiteDB.BsonIgnore]
        public PixPayload Payload => _pixPayloadService?.Create(this) ?? new PixPayload();

        [LiteDB.BsonIgnore]
        public PixKeyCommand Command => _pixKeyCommand?.Create(this) ?? new PixKeyCommand();

        [LiteDB.BsonIgnore]
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
