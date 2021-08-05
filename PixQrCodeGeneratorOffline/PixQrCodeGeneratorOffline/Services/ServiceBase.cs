using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Services
{
    public class ServiceBase
    {
        protected IUserDialogs DialogService => UserDialogs.Instance;

        protected readonly IEventService _eventService;

        public ServiceBase()
        {
            _eventService = DependencyService.Get<IEventService>();
        }
    }
}
