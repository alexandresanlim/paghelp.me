using PixQrCodeGeneratorOffline.Models.Validation.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Validation
{
    public class PixKeyValidation : ValidationBase
    {
        public bool HasKey { get; set; }

        public bool HasName { get; set; }
    }
}
