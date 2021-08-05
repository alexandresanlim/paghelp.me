using PixQrCodeGeneratorOffline.Base.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionKeyViewModel : ViewModelBase
    {
        public OptionKeyViewModel()
        {
            LoadDataCommand.Execute(null);
        }

        public ICommand LoadDataCommand => new Command(() =>
        {

        });

        public ICommand ShareAllCommand => new Command(() =>
        {
            _pixKeyService.ShareAllKeys();
        });

        public ICommand RemoveAllCommand => new Command(async () =>
        {
            var success = await _pixKeyService.RemoveAll();

            if (success)
            {
                DashboardVM.PixKeyList.Clear();
                await DashboardVM.LoadCurrentPixKey(null);
                await NavigateToRootAsync();
            }
        });

        //private async Task OptionsKeysOpen()
        //{
        //    if (PixKeyList == null || PixKeyList.Count.Equals(0))
        //    {
        //        await DialogService.AlertAsync("Adicione pelo menos 1(uma) chave para ver opções.");
        //        return;
        //    }

        //    var keys = new List<Acr.UserDialogs.ActionSheetOption>
        //    {
        //        new Acr.UserDialogs.ActionSheetOption("Campartilhar todas as chaves", () =>
        //        {
        //            _pixKeyService.ShareAllKeys();
        //        })
        //    };

        //    if (PixKeyList.Count > 1)
        //    {
        //        keys.Add(new Acr.UserDialogs.ActionSheetOption($"Excluir todas as {PixKeyList.Count} chaves", async () =>
        //        {
        //            var success = await _pixKeyService.RemoveAll();

        //            if (success)
        //            {
        //                PixKeyList.Clear();
        //                await LoadCurrentPixKey(null);
        //            }
        //        }));
        //    }

        //    DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
        //    {
        //        Title = "Chaves",
        //        Options = keys,
        //        Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
        //        {
        //            return;
        //        })
        //    });
        //}
    }
}
