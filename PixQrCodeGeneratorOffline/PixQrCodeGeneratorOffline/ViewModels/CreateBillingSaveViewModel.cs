using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class CreateBillingSaveViewModel : ViewModelBase
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

        private void LoadPixPayloadSave()
        {
            BillingSaveList = _pixPayloadService?.GetAll(x => x.PixKey.Id == CurrentPixKey.Id)?.ToObservableCollection() ?? new ObservableCollection<PixPayload>();
        }

        private ObservableCollection<PixPayload> _billingSaveList;
        public ObservableCollection<PixPayload> BillingSaveList
        {
            set => SetProperty(ref _billingSaveList, value);
            get => _billingSaveList;
        }

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }
    }
}
