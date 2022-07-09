using PixQrCodeGeneratorOffline.Models.Commands.Base;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;

namespace PixQrCodeGeneratorOffline.Models.Commands.Interfaces
{
    public interface IPayloadCommandBase
    {
        PayloadCommandBase Create(PayloadBase payloadBase);
    }
}
