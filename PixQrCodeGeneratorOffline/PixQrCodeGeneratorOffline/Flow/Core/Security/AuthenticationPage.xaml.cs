
using Acr.UserDialogs;
using AsyncAwaitBestPractices;
using PixQrCodeGeneratorOffline.Extention;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Flow.Core.Security
{
    public partial class AuthenticationPage : PopupPage
    {
        Action currentAction;

        public AuthenticationPage(Action execute)
        {
            currentAction = execute;
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var request = new AuthenticationRequestConfiguration("Autenticação", "Atentique-se para continuar e ver suas chaves");

                var result = await CrossFingerprint.Current.AuthenticateAsync(request);

                if (result.Authenticated)
                {
                    await Shell.Current.Navigation.PopPopupAsync();
                    UserDialogs.Instance.Toast("Autenticado com sucesso!");
                    currentAction.Invoke();
                }
                else
                {
                    UserDialogs.Instance.Toast("Não autenticado");
                }
            }
            catch (Exception ex)
            {
                ex.SendToLog();
            }
        }
    }
}