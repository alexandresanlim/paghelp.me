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
        public BillingSaveListViewModel()
        {
            LoadPixPayloadSave();
        }

        private void LoadPixPayloadSave()
        {
            BillingSaveList = _pixPayloadService?.GetAll()?.ToObservableCollection() ?? new ObservableCollection<PixPayload>();
        }

        private ObservableCollection<PixPayload> _billingSaveList;
        public ObservableCollection<PixPayload> BillingSaveList
        {
            set => SetProperty(ref _billingSaveList, value);
            get => _billingSaveList;
        }
    }
}
