using AsyncAwaitBestPractices.MVVM;
using PixQrCodeGeneratorOffline.Base.ViewModels;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class ShareKeyViewModel : ViewModelBase
    {
        public ICommand LoadDataCommand => new Command(() => LoadData());

        public ICommand ShareCommand => new Command(Share);

        public IAsyncCommand CopyCommand => new AsyncCommand(Copy);

        public ShareKeyViewModel(ObservableCollection<PixKey> pixKeyList)
        {
            PixKeyList = pixKeyList;
            LoadDataCommand.Execute(null);
        }

        public void LoadData(ShareKeyLoadDataParameter shareKeyLoadDataParameter = null)
        {
            PreviewText = string.Empty;

            foreach (PixKey item in PixKeyList)
            {
                if (shareKeyLoadDataParameter != null)
                {
                    string institutionText = shareKeyLoadDataParameter.Institution ? item?.FinancialInstitution?.Name + " " : "";
                    string pointText = shareKeyLoadDataParameter.Point ? ";" : "";
                    string sameLine = shareKeyLoadDataParameter.SameLine ? "" : "\n";
                    string separator = shareKeyLoadDataParameter.AddSeparator ? !string.IsNullOrWhiteSpace(institutionText) ? "| " : "" : "";

                    string institutionTitle = !string.IsNullOrWhiteSpace(institutionText) ? (shareKeyLoadDataParameter.AddDescription ? "Instituição: " : "") : "";
                    string keyTitle = shareKeyLoadDataParameter.AddDescription ? "Chave: " : "";

                    PreviewText += string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        institutionTitle, institutionText, separator, keyTitle, item.Key, pointText, sameLine);
                }

                else
                {
                    PreviewText += string.Format("{0}{1}", item.Key, "\n");
                }
            }
        }

        private void Share() => _pixKeyService.ShareAllKeys(PreviewText);

        private async Task Copy() => await _externalActionService.CopyText(PreviewText);

        private ObservableCollection<PixKey> _pixKeyList;
        public ObservableCollection<PixKey> PixKeyList
        {
            set => SetProperty(ref _pixKeyList, value);
            get => _pixKeyList;
        }

        private string _previewText;
        public string PreviewText
        {
            set => SetProperty(ref _previewText, value);
            get => _previewText;
        }
    }

    public class ShareKeyLoadDataParameter
    {
        public bool Institution { get; set; }

        public bool Point { get; set; }

        public bool SameLine { get; set; }

        public bool AddSeparator { get; set; }

        public bool AddDescription { get; set; }
    }
}
