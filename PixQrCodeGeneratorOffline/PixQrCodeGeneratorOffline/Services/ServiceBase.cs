using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Services
{
    public class ServiceBase
    {
        protected IUserDialogs DialogService => UserDialogs.Instance;

        protected readonly IEventService _eventService;

        private bool _isLoading;

        public ServiceBase()
        {
            _eventService = DependencyService.Get<IEventService>();
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
