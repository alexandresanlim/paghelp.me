using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces
{
    public interface IPixCobValidationService
    {
        PixCobValidation Create(PixCob pixCob);
    }
}
