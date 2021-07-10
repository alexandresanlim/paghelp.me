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

            var options = new List<ActionSheetOption>
            {
                new ActionSheetOption((Preference.FingerPrint ? "Remover" : "Adicionar") + " autenticação biométrica", async () =>
                {
                     var confirmMsg = "Tem certeza que deseja " + (Preference.FingerPrint ? "remover" : "adicionar") + " autenticação biométrica? Na próxima vez que você entrar, " + (Preference.FingerPrint ? "não será" : "será") + " necessário se autenticar para realizar quaisquer ações.";

                    if (!await DialogService.ConfirmAsync(confirmMsg, "Confirmação", "Confirmar", "Cancelar"))
                        return;

                    Preference.FingerPrint = !Preference.FingerPrint;

                    DialogService.Toast("Preferência de entrada, salva com sucesso!");

                })
            };

            DialogService.ActionSheet(new ActionSheetConfig
            {
                Title = "Proteção por biometria",
                Options = options,
                Cancel = new ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        }

        public async Task ChangePDVMode()
        {
            var options = new List<ActionSheetOption>
            {
                new ActionSheetOption((Preference.IsPDVMode ? "Desativar" : "Ativar") + " modo PDV", async () =>
                {
                     var confirmMsg = "Tem certeza que deseja " + (Preference.IsPDVMode ? "desativar" : "ativar") + " o modo PDV? Na próxima vez que você entrar, o app " + (Preference.IsPDVMode ? "não será" : "será") + " aberto em tela cheia e se manterá ligada.";

                    if (!await DialogService.ConfirmAsync(confirmMsg, "Confirmação", "Sim", "Cancelar"))
                        return;

                    Preference.IsPDVMode = !Preference.IsPDVMode;

                    DialogService.Toast("Preferência do modo PDV, salvo com sucesso!");

                })
            };

            DialogService.ActionSheet(new ActionSheetConfig
            {
                Title = "Modo PDV",
                Options = options,
                Cancel = new ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        }
    }
}
