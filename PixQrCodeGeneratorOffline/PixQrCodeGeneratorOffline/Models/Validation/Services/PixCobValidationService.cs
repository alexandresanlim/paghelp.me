using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services
{
    public class PixCobValidationService : IPixCobValidationService
    {
        public PixCobValidation Create(PixCob pixCob)
        {
            return new PixCobValidation
            {
                IsValid = GetIsValid(pixCob),
                HasValue = GetHasValue(pixCob)
            };
        }

        private bool GetIsValid(PixCob pixCob)
        {
            return true;
        }

        private bool GetHasValue(PixCob pixCob)
        {
            return !string.IsNullOrEmpty(pixCob?.Value) && !pixCob.Value.Equals("0.00") && !pixCob.Value.Equals("0,00");
        }
    }
}
