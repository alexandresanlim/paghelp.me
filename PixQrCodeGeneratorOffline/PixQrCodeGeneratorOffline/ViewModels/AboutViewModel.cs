using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
        {
            Title = "Sobre";

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

        public ICommand OpenGithubCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Xamarin.Essentials.Browser.OpenAsync("https://github.com/pixqrcodegeneratoroffline/PixQrCodeGeneratorOffline");
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Abriu repositório Github");

                SetIsLoading(false);
            }
        });

        public ICommand SupportCommand => new Command(() =>
        {
            try
            {
                var options = new List<Acr.UserDialogs.ActionSheetOption>
                {
                    new Acr.UserDialogs.ActionSheetOption("Avaliar na " + App.Info.StoreNameByDeviceInfo, async () =>
                    {
                        await App.OpenAppInStore();
                    }),
                    new Acr.UserDialogs.ActionSheetOption("Compartilhar", async () =>
                    {
                        await _externalActionService.ShareText(App.Info.StoreTextToShare);
                    }),
                    new Acr.UserDialogs.ActionSheetOption("Copiar código copia e cola pix para doação", async () =>
                    {
                        await _externalActionService.CopyText("00020126760014br.gov.bcb.pix0136bee05743-4291-4f3c-9259-595df1307ba10214Doação PIX APP5204000053039865802BR5909Alexandre6008Curitiba62200516PIXOFFe2d72825e26304208D", "Código copiado com sucesso!");
                    })
                };

                DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
                {
                    Title = "Selecione uma opção",
                    Options = options,
                    Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                    {
                        return;
                    })
                });
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Tocou em doação", Services.EventType.TAP);
            }
        });

        public ICommand OpenStoreCommand => new Command(async () =>
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
                _eventService.SendEvent("Tocou em avaliar", Services.EventType.TAP);
            }
        });

        public ICommand OpenInstagramCommand => new Command(async () =>
        {
            try
            {
                await App.OpenAppIntagram();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                _eventService.SendEvent("Tocou em ver instagram", Services.EventType.TAP);
            }
        });

        public ICommand SendAMessageCommand => new Command(async () =>
        {
            var text = await DialogService.PromptAsync("Sugestão, elogio ou crítica? Fale pra nós o que você está achando...", "Feedback", "Enviar", "Cancelar");

            if (text.Ok && !string.IsNullOrWhiteSpace(text?.Text))
            {
                var dic = new Dictionary<string, string>
                {
                    { "Texto: ", text.Text }
                };

                var contact = await DialogService.PromptAsync("Queremos te responder :)", "Qual o seu e-mail?", "Enviar", "Não quero informar");

                if (contact.Ok && string.IsNullOrWhiteSpace(contact?.Text))
                    dic.Add("Contato", contact.Text);

                _eventService.SendEvent("Sugestão: ", Services.EventType.FEEDBACK, dic);

                DialogService.Toast("Mensagem enviada com sucesso! Obrigado.");
            }
        });

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