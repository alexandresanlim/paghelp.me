using Microsoft.AppCenter.Analytics;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Services
{
    public class EventService : IEventService
    {
        public void SendEvent(string text, IDictionary<string, string> properties = null)
        {
            Analytics.TrackEvent(text, properties);
        }
    }
}
