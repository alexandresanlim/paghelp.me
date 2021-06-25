using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Services
{
    public class PreferenceService : IPreferenceService
    {
        private IUserDialogs DialogService => UserDialogs.Instance;

        protected readonly IPixKeyService _pixKeyService;

        public PreferenceService()
        {
            _pixKeyService = DependencyService.Get<IPixKeyService>();
        }

        public void ChangeHideData()
        {
            Preference.HideData = !Preference.HideData;
        }

        public void ChangeShowInList()
        {
            var keys = _pixKeyService.GetAll();

            if (keys == null && !(keys.Count > 0))
            {
                DialogService.Toast("Não é possível alterar está opção, pois nenhuma chave foi encontrada.");
                return;
            }

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
