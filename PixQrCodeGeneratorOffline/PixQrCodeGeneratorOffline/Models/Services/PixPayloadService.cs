using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixPayloadService : IPixPayloadService
    {
        private readonly IPixKeyService _pixKeyService;

        private readonly IPixCobService _pixKCobService;

        public PixPayloadService()
        {
            _pixKeyService = DependencyService.Get<IPixKeyService>();
            _pixKCobService = DependencyService.Get<IPixCobService>();
        }

        public PixPayload Create(PixKey pixKey)
        {
            if (!_pixKeyService.IsValid(pixKey))
                return new PixPayload();

            var pixPaylod = new PixPayload
            {
                PixKey = pixKey
            };

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                Cobranca cobranca = new Cobranca(_chave: pixKey.Key);

                pixPaylod.Payload = cobranca?.ToPayload("PIXAPP" + Guid.NewGuid().ToString("N").Substring(0, 10), new Merchant(pixKey?.Name, pixKey?.City));

                pixPaylod.QrCode = pixPaylod.Payload?.GenerateStringToQrCode();
            });

            return pixPaylod;
        }

        public PixPayload Create(PixKey pixKey, PixCob pixCob)
        {
            if (!_pixKeyService.IsValid(pixKey) || !_pixKCobService.IsValid(pixCob))
                return new PixPayload();

            var pixPaylod = new PixPayload
            {
                PixKey = pixKey,
                PixCob = pixCob
            };

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                Cobranca cobranca = new Cobranca(_chave: pixKey.Key)
                {
                    SolicitacaoPagador = pixCob?.Description,
                    Valor = new Valor
                    {
                        Original = pixCob.ValueFormatter
                    }
                };

                pixPaylod.Payload = cobranca?.ToPayload("PIXAPP" + Guid.NewGuid().ToString("N").Substring(0, 10), new Merchant(pixKey?.Name, pixKey?.City));

                pixPaylod.QrCode = pixPaylod.Payload?.GenerateStringToQrCode();
            });

            return pixPaylod;
        }

        public bool IsValid(PixPayload pixPayload)
        {
            return !string.IsNullOrEmpty(pixPayload?.QrCode);
        }
    }
}
