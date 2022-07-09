using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixCobService : ServiceBase, IPixCobService
    {
        public PixCob Create(string value, string description = "")
        {
            return new PixCob
            {
                Description = description,
                Value = value
            };
        }    
    }
}
