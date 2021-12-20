using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixCobService : ServiceBase, IPixCobService
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
