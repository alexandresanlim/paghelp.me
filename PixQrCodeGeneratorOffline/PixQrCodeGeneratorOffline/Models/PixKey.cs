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
        public List<PixKeyAction> Actions => PixKeyAction.GetList(this);
    }

    public class PixKeyAction
    {
        public string Icon { get; set; }

        public string Title { get; set; }

        public System.Windows.Input.ICommand Command { get; set; }

        public MaterialColor Colors { get; set; }

        public static List<PixKeyAction> GetList(PixKey pixKey)
        {
            return new List<PixKeyAction>
            {
                new PixKeyAction
                {
                    Title = "Criar Qr Code",
                    Icon = FontAwesomeSolid.Qrcode,
                    Command = pixKey?.Command?.NavigateToCreateBillingPageCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Copiar",
                    Icon = FontAwesomeSolid.Copy,
                    Command = pixKey?.Command?.CopyKeyCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Compartilhar",
                    Icon = FontAwesomeSolid.ShareAlt,
                    Command = pixKey?.Command?.ShareKeyCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Cobranças Salvas",
                    Icon = FontAwesomeSolid.HandHoldingUsd,
                    Command = pixKey?.Command?.NavigateToBillingCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Editar",
                    Icon = FontAwesomeSolid.Pen,
                    Command = pixKey?.Command?.EditKeyCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                
                //new PixKeyAction
                //{
                //    Title = "Excluir",
                //    Icon = FontAwesomeSolid.TrashAlt,
                //    Command = pixKey?.Command?.EditKeyCommand,
                //    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                //}
            };
        }
    }

    public static class PixKeyExtention
    {
        public static bool IsAKey(this string key)
        {
            return GetKeyType(key) != PixKeyType.NotFound;
        }

        public static PixKeyType GetKeyType(this string key)
        {
            if (key.IsEmail())
                return PixKeyType.Email;

            if (key.IsCPF())
                return PixKeyType.CPF;

            if (key.IsCNPJ())
                return PixKeyType.CNPJ;

            if (key.IsGuid())
                return PixKeyType.Aleatoria;

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
