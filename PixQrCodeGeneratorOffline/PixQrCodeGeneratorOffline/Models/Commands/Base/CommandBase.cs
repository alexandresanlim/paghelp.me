using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Commands.Base
{
    public class CommandBase
    {
        protected IUserDialogs DialogService => UserDialogs.Instance;

        protected readonly IEventService _eventService;

        protected readonly IExternalActionService _externalActionService;

        protected readonly ICustomAsyncCommand _customAsyncCommand;

        protected bool _isLoading;

        public CommandBase()
        {
            _eventService = DependencyService.Get<IEventService>();
            _externalActionService = DependencyService.Get<IExternalActionService>();
            _customAsyncCommand = DependencyService.Get<ICustomAsyncCommand>();
        }

        public async Task NavigateBackPopupAsync()
        {
            await Shell.Current.Navigation.PopPopupAsync();
        }

        public void SetIsLoading(bool isLoading = true, string title = "")
        {
            _isLoading = isLoading;

            if (_isLoading)
                DialogService.ShowLoading(title);

            else
                DialogService.HideLoading();
        }
    }
}
