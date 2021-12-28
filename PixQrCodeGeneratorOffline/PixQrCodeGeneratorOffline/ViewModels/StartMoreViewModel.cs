using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class StartMoreViewModel : ViewModelBase
    {
        public string CurrentVersion => App.Info.VersionString;

        #region Commands

        public IAsyncCommand NavigateToPreferencesCommand => new AsyncCommand(async () => await NavigateAsync(new OptionPreferencePage()));

        public IAsyncCommand NavigateBenefitsCommand => new AsyncCommand(async () => await NavigateAsync(new BenefitsPage(true)));

        public IAsyncCommand LoadDataCommand => new AsyncCommand(LoadData);

        public IAsyncCommand SendAMessageCommand => new AsyncCommand(SendAMessageAsync);

        public IAsyncCommand OpenStoreCommand => new AsyncCommand(OpenStoreAsync);

        #endregion

        private async Task LoadData()
        {
            if (App.DeviceInfo.IsAndroid)
            {
                CurrentStore = "Avaliar na Google Play";
                CurrentStoreIcon = FontAwesomeBrands.GooglePlay;
            }

            else
            {
                CurrentStore = "Avaliar na App Store";
                CurrentStoreIcon = FontAwesomeBrands.AppStore;
            }
        }

        private async Task SendAMessageAsync()
        {
            var text = await DialogService.PromptAsync("Sugestão, elogio ou crítica? Fale para nós o que você está achando:", "Feedback", "Enviar", "Cancelar");

            if (text.Ok && !string.IsNullOrWhiteSpace(text?.Text))
            {
                var dic = new Dictionary<string, string>
                {
                    { "Texto: ", text.Text }
                };

                var contact = await DialogService.PromptAsync("Queremos te responder :)", "Qual o seu e-mail?", "Enviar", "Não quero informar");

                if (contact.Ok && !string.IsNullOrWhiteSpace(contact?.Text))
                    dic.Add("Contato", contact.Text);

                _eventService.SendEvent("Sugestão: ", Services.EventType.FEEDBACK, nameof(StartMoreViewModel), dic);

                DialogService.Toast("Mensagem enviada com sucesso! Obrigado.");
            }
        }

        private async Task OpenStoreAsync()
        {
            try
            {
                await App.OpenAppInStore();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Tocou em avaliar", Services.EventType.TAP, nameof(StartMoreViewModel));
            }
        }

        private string _currentStore;
        public string CurrentStore
        {
            set => SetProperty(ref _currentStore, value);
            get => _currentStore;
        }

        private string _currentStoreIcon;
        public string CurrentStoreIcon
        {
            set => SetProperty(ref _currentStoreIcon, value);
            get => _currentStoreIcon;
        }
    }
}
