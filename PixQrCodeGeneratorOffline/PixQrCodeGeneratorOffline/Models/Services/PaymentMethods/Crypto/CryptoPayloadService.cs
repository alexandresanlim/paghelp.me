using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto
{
    public class CryptoPayloadService : ServiceBase, ICryptoPayloadService
    {
        private readonly ICryptoKeyService _pixKeyService;

        //private readonly ICryptoCobService _pixKCobService;

        //private readonly ICryptoPayloadRepository _pixPayloadRepository;

        public CryptoPayloadService()
        {
            _pixKeyService = DependencyService.Get<ICryptoKeyService>();
            //_pixKCobService = DependencyService.Get<ICryptoCobService>();
            //_pixPayloadRepository = DependencyService.Get<ICryptoPayloadRepository>();
        }

        public CryptoPayload Create(CryptoKey pixKey)
        {

            if (!_pixKeyService.IsValid(pixKey))
                return new CryptoPayload();

            var payload = new CryptoPayload
            {
                CryptoKey = pixKey
            };

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                //Cobranca cobranca = new Cobranca(_chave: pixKey.Key);

                //pixPaylod.Payload = cobranca?.ToPayload("PAGHELPME" + Guid.NewGuid().ToString("N").Substring(0, 10), new Merchant(pixKey?.Name, pixKey?.City));

                payload.QrCode = payload?.GenerateStringToQrCode();
            });

            return payload;
        }

        public CryptoPayload Create(CryptoKey pixKey, CryptoCob pixCob)
        {
            return null;
            //throw new NotImplementedException();
        }

        public List<CryptoPayload> GetAll(Expression<Func<CryptoPayload, bool>> predicate = null)
        {
            return null;
            //throw new NotImplementedException();
        }

        public bool IsValid(CryptoPayload pixPayload)
        {
            return false;
            //throw new NotImplementedException();
        }

        public Task<bool> RemoveAll(Expression<Func<CryptoPayload, bool>> predicate = null)
        {
            return Task.FromResult(false);
            //throw new NotImplementedException();
        }

        public bool Save(CryptoPayload pixPaylod)
        {
            return false;
            //throw new NotImplementedException();
        }
    }

    public static class CryptoPayloadExtensions
    {
        public static string GenerateStringToQrCode(this CryptoPayload cryptoPayload)
        {
            var payload = $"bitcoin:{cryptoPayload?.CryptoKey?.Key}";

            if(!string.IsNullOrWhiteSpace(cryptoPayload?.Amount))
            {
                payload += "?amount=.01%26label=Moloch.net%26message=Donation";
            }

            return payload;
        }
    }
}
