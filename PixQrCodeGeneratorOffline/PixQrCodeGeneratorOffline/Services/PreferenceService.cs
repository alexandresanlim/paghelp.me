using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using Plugin.Fingerprint;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Services
{
    public class PreferenceService : ServiceBase, IPreferenceService
    {
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

            try
            {
                Preference.ShowInList = !Preference.ShowInList;

                _eventService.SendEvent($"Mudou estilo da exibição da lista, {nameof(Preference.ShowInList)} : {Preference.ShowInList}", EventType.PREFERENCE);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        public async Task<bool> ChangeFingerPrint()
        {
            if (!await CrossFingerprint.Current.IsAvailableAsync())
            {
                DialogService.Toast("Está função esta desativa ou não disponível para o seu dispositivo");
                return false;
            }

            try
            {
                Preference.FingerPrint = !Preference.FingerPrint;

                DialogService.Toast("Preferência de entrada, salva com sucesso!");

                _eventService.SendEvent($"Mudou entrada por fingerprint, {nameof(Preference.FingerPrint)} : {Preference.FingerPrint}", EventType.PREFERENCE);

                return true;
            }
            catch (Exception e)
            {
                e.SendToLog();
                return false;
            }
        }

        public async Task ChangePDVMode()
        {
            try
            {
                Preference.IsPDVMode = !Preference.IsPDVMode;

                DialogService.Toast("Preferência do modo PDV, salvo com sucesso!");

                _eventService.SendEvent($"Mudou modo PDV, {nameof(Preference.IsPDVMode)} : {Preference.IsPDVMode}", EventType.PREFERENCE);

            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        public async Task ChangeShowNewsMode()
        {
            try
            {
                Preference.ShowNews = !Preference.ShowNews;

                DialogService.Toast("Preferência de exibir notícias, salvo com sucesso!");

                _eventService.SendEvent($"Mudou exibir notícias, {nameof(Preference.ShowNews)} : {Preference.ShowNews}", EventType.PREFERENCE);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }

        public async Task ChangeTheme()
        {
            try
            {
                Preference.ThemeIsDark = !Preference.ThemeIsDark;
                App.LoadTheme();

                _eventService.SendEvent($"Mudou tema, {nameof(Preference.ThemeIsDark)} : {Preference.ThemeIsDark}", EventType.PREFERENCE);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        }
    }
}
