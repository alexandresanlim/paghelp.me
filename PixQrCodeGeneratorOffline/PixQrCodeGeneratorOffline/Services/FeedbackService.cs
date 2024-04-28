using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using Xamarin.Essentials;

namespace PixQrCodeGeneratorOffline.Services
{
    public class FeedbackService : IFeedbackService
    {
        public void Feedback(HapticFeedbackType type = HapticFeedbackType.Click)
        {
            try 
            { 
                HapticFeedback.Perform(type); 
            } catch (Exception) { }
        }
    }
}
