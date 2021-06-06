using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class GuideService : IGuideService
    {
        public Guide Create(string _question, string _answer)
        {
            return new Guide
            {
                Question = _question,
                Answer = _answer
            };
        }
    }
}
