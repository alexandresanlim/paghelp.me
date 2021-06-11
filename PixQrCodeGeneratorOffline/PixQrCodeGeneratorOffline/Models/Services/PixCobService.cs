using pix_payload_generator.net.Models.CobrancaModels;
using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixCobService : IPixCobService
    {
        private readonly IPixKeyService _pixKeyService;

        public PixCobService()
        {
            _pixKeyService = DependencyService.Get<IPixKeyService>();
        }

        public PixCob Create(string value, string description = "")
        {
            return new PixCob
            {
                Description = description,
                Value = value
            };
        }

        public bool IsValid(PixCob pixCob)
        {
            return !string.IsNullOrEmpty(pixCob?.Value);
        }        
    }
}
