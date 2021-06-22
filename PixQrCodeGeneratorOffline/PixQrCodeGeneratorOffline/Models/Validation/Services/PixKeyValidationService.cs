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
                IsValid = GetIsValid(pixKey),
                HasKey = GetHasKey(pixKey),
                HasName = GetHasName(pixKey)
            };
        }

        private bool GetIsValid(PixKey pixKey)
        {
            return pixKey != null && !string.IsNullOrWhiteSpace(pixKey?.Key);
        }

        private bool GetHasKey(PixKey pixKey)
        {
            return !string.IsNullOrWhiteSpace(pixKey?.Key);
        }

        private bool GetHasName(PixKey pixKey)
        {
            return !string.IsNullOrWhiteSpace(pixKey?.Name);
        }
    }
}
