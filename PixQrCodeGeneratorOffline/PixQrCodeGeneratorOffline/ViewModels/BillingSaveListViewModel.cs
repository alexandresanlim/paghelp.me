using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class BillingSaveListViewModel : ViewModelBase
    {
        public BillingSaveListViewModel(Models.PixKey pixKey = null)
        {
            CurrentPixKey = pixKey ?? new PixKey();

            LoadPixPayloadSave();
        }

        private void LoadPixPayloadSave()
        {
            BillingSaveList = CurrentPixKey.Validation.IsValid ?
                _pixPayloadService?.GetAll(x => x.PixKey.Id == CurrentPixKey.Id)?.ToObservableCollection() ?? new ObservableCollection<PixPayload>() :
                _pixPayloadService?.GetAll()?.ToObservableCollection() ?? new ObservableCollection<PixPayload>();
        }

        private PixKey CurrentPixKey { get; set; }

        private ObservableCollection<PixPayload> _billingSaveList;
        public ObservableCollection<PixPayload> BillingSaveList
        {
            set => SetProperty(ref _billingSaveList, value);
            get => _billingSaveList;
        }
    }
}
