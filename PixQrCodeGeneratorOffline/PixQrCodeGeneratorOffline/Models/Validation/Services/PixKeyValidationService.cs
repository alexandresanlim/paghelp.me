using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services
{
    public class PixKeyValidationService : IPixKeyValidationService
    {
        public PixKeyValidation Create(PixKey pixKey)
        {
            return new PixKeyValidation
            {
                IsValid = GetIsValid(pixKey)
            };
        }

        private bool GetIsValid(PixKey pixKey)
        {
            return pixKey != null && !string.IsNullOrWhiteSpace(pixKey?.Key);
        }
    }
}
