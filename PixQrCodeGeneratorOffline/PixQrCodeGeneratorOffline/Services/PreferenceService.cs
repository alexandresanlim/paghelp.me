using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Services
{
    public class PreferenceService : IPreferenceService
    {
        private IUserDialogs DialogService => UserDialogs.Instance;

        public void ChangeHideData()
        {
            Preference.HideData = !Preference.HideData;
        }

        public void ChangeShowInList()
        {
            Preference.ShowInList = !Preference.ShowInList;
        }

        public async Task ChangeFingerPrint()
        {
            if (!await CrossFingerprint.Current.IsAvailableAsync())
            {
                DialogService.Toast("Está função esta desativa ou não disponível para o seu dispositivo");
                return;
            }

            var confirmMsg = "Tem certeza que deseja " + (Preference.FingerPrint ? "remover" : "adicionar") + " autenticação biométrica? Na próxima vez que você entrar, " + (Preference.FingerPrint ? "não será" : "será") + " necessário se autenticar para realizar quaisquer ações.";

            if (!await DialogService.ConfirmAsync(confirmMsg, "Confirmação", "Confirmar", "Cancelar"))
                return;

            Preference.FingerPrint = !Preference.FingerPrint;

            DialogService.Toast("Preferência de entrada, salva com sucesso!");
        }
    }
}
