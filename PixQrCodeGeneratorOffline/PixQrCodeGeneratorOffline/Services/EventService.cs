using Microsoft.AppCenter.Analytics;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System.Collections.Generic;

namespace PixQrCodeGeneratorOffline.Services
{
    public class EventService : IEventService
    {
        public void SendEvent(string text, EventType eventType = EventType.OUTHER, string fromClass = "", IDictionary<string, string> properties = null)
        {
            if(!string.IsNullOrWhiteSpace(fromClass))
            {
                properties = new Dictionary<string, string>
                {
                    { "Class", fromClass }
                };
            }

            Analytics.TrackEvent($"[{eventType}] {text}", properties);
        }
    }

    public enum EventType
    {
        OUTHER,
        SHARE,
        PREFERENCE,
        NAVIGATION,
        SEE,
        FEEDBACK,
        CREATE,
        READ,
        UPDATE,
        DELETE,
        TAP
    }
}
