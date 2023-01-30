using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class CreateBillingSaveViewModel : BillingSaveListViewModel
    {

        public Command<PixKey> LoadDataCommand => new Command<PixKey>((pixKey) =>
        {
            try
            {
                CurrentPixKey = pixKey;

                LoadPixPayloadSave();
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
        });
    }
}
