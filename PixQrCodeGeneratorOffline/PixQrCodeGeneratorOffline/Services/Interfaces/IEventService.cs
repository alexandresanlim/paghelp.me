using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IEventService
    {
        void SendEvent(string text, EventType eventType = EventType.OUTHER, IDictionary<string, string> properties = null);
    }
}
