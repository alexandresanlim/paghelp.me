using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Views;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class StartMoreViewModel : ViewModelBase
    {
        #region Commands

        public IAsyncCommand NavigateToPreferencesCommand => new AsyncCommand(async () => await NavigateAsync(new OptionPreferencePage()));

        public IAsyncCommand NavigateToAboutCommand => new AsyncCommand(async () => await NavigateAsync(new AboutPage()));

        public IAsyncCommand NavigateBenefitsCommand => new AsyncCommand(async () => await NavigateAsync(new BenefitsPage(true)));

        #endregion
    }
}
