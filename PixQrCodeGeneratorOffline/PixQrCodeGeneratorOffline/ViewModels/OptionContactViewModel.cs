using PixQrCodeGeneratorOffline.Base.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class OptionContactViewModel : ViewModelBase
    {
        public ICommand RemoveAllCommand => new Command(async () =>
        {
            var success = await _pixKeyService.RemoveAll(isContact: true);

            if (success)
            {
                DashboardContactVM.PixKeyList.Clear();
                await DashboardContactVM.LoadCurrentPixKey(null);
                NavigateBack();
            }
        });
    }
}
