using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands.Base
{
    public class CommandBase
    {
        protected IUserDialogs DialogService => UserDialogs.Instance;

        protected readonly IEventService _eventService;

        public CommandBase()
        {
            _eventService = DependencyService.Get<IEventService>();
        }
    }
}
