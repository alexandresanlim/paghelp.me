using Xamarin.Essentials;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IFeedbackService
    {
        void Feedback(HapticFeedbackType type = HapticFeedbackType.Click);
    }
}
