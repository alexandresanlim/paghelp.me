using Acr.UserDialogs;
using PixQrCodeGeneratorOffline.Extention;
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
        protected readonly IEventService _eventService;

        public PreferenceService()
        {
            _pixKeyService = DependencyService.Get<IPixKeyService>();
            _eventService = DependencyService.Get<IEventService>();
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

            try
            {
                Preference.ShowInList = !Preference.ShowInList;

                DialogService.Toast("Preferência de exibição, salva com sucesso!");

                _eventService.SendEvent($"Mudou estilo da exibição da lista, {nameof(Preference.ShowInList)} : {Preference.ShowInList}", EventType.PREFERENCE);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        public async Task ChangeFingerPrint()
        {
            if (!await CrossFingerprint.Current.IsAvailableAsync())
            {
                DialogService.Toast("Está função esta desativa ou não disponível para o seu dispositivo");
                return;
            }

            try
            {
                var options = new List<ActionSheetOption>
                {
                    new ActionSheetOption((Preference.FingerPrint ? "Remover" : "Adicionar") + " autenticação biométrica", async () =>
                    {
                         var confirmMsg = "Tem certeza que deseja " + (Preference.FingerPrint ? "remover" : "adicionar") + " autenticação biométrica? Na próxima vez que você entrar, " + (Preference.FingerPrint ? "não será" : "será") + " necessário se autenticar para realizar quaisquer ações.";

                        if (!await DialogService.ConfirmAsync(confirmMsg, "Confirmação", "Confirmar", "Cancelar"))
                            return;

                        Preference.FingerPrint = !Preference.FingerPrint;

                        DialogService.Toast("Preferência de entrada, salva com sucesso!");

                        _eventService.SendEvent($"Mudou entrada por fingerprint, {nameof(Preference.FingerPrint)} : {Preference.FingerPrint}", EventType.PREFERENCE);
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
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        public async Task ChangePDVMode()
        {
            try
            {
                var options = new List<ActionSheetOption>
                {
                    new ActionSheetOption((Preference.IsPDVMode ? "Desativar" : "Ativar") + " modo PDV", async () =>
                    {
                         var confirmMsg = "Tem certeza que deseja " + (Preference.IsPDVMode ? "desativar" : "ativar") + " o modo PDV? Na próxima vez que você entrar, o app " + (Preference.IsPDVMode ? "não será" : "será") + " aberto em tela cheia e se manterá ligado. Ideal para locais com auto recorrência de vendas.";

                        if (!await DialogService.ConfirmAsync(confirmMsg, "Confirmação", "Sim", "Cancelar"))
                            return;

                        Preference.IsPDVMode = !Preference.IsPDVMode;

                        DialogService.Toast("Preferência do modo PDV, salvo com sucesso!");

                        _eventService.SendEvent($"Mudou modo PDV, {nameof(Preference.IsPDVMode)} : {Preference.IsPDVMode}", EventType.PREFERENCE);
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
            catch (Exception e)
            {
                e.SendToLog();
            }
        }
    }
}
