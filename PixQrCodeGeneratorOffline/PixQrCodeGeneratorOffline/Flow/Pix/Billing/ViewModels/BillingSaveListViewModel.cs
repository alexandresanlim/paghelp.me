using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using System.Collections.ObjectModel;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class BillingSaveListViewModel : ViewModelBase
    {
        public BillingSaveListViewModel(PixKey pixKey = null)
        {
            CurrentPixKey = pixKey ?? new PixKey();

            LoadPixPayloadSave();
        }

        protected void LoadPixPayloadSave()
        {
            if (!CurrentPixKey.IsValid() || !BillingSaveList.IsNullOrEmpty())
                return;

            BillingSaveList = _pixPayloadService?.GetAll(x => x.PixKey.Id == CurrentPixKey.Id)?.ToObservableCollection() ?? new ObservableCollection<PixPayload>();
        }

        protected PixKey CurrentPixKey { get; set; }

        private ObservableCollection<PixPayload> _billingSaveList;
        public ObservableCollection<PixPayload> BillingSaveList
        {
            set => SetProperty(ref _billingSaveList, value);
            get => _billingSaveList;
        }
    }
}
