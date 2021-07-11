using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Services
{
    public class ServiceBase
    {
        protected IUserDialogs DialogService => UserDialogs.Instance;
    }
}
