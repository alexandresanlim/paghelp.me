using PixQrCodeGeneratorOffline.Models.Services.Interfaces;

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
