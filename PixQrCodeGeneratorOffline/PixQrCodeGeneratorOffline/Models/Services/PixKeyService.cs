using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixKeyService : IPixKeyService
    {
        public bool IsValid(PixKey pixKey)
        {
            return !string.IsNullOrWhiteSpace(pixKey?.Key);
        }
    }
}
