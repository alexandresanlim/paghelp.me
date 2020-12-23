using pix_payload_generator.net.Models.PayloadModels;
using PixQrCodeGeneratorOffline.DataBase;
using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services;
using PixQrCodeGeneratorOffline.Style;
using PixQrCodeGeneratorOffline.Style.Interfaces;
using PixQrCodeGeneratorOffline.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public DashboardViewModel()
        {
            LoadDataCommand.Execute(null);
        }

        public Command LoadDataCommand => new Command(async () =>
        {
            //try
            //{
            //    SetIsLoading(true);

            await Task.Delay(1000);

            var list = PixKeyDataBase.GetAll();

            list.Add(new PixKey
            {
                Color = MaterialColor.GetRandom()
            });

            list.Select(x =>
            {
                x.RaisePresentation();
                return x;
            }).ToList();

            PixKeyList = list.ToObservableCollection();

            await LoadCurrentPixKey();

            //await LoadHideData();
            //}
            //catch (System.Exception)
            //{

            //    throw;
            //}
            //finally
            //{
            //    SetIsLoading(false);
            //}
        });

        public async Task LoadCurrentPixKey(PixKey pixkey = null)
        {
            if (PixKeyList == null || !(PixKeyList.Count > 0))
                return;

            CurrentPixKey = pixkey ?? PixKeyList.FirstOrDefault();

            //await SetStatusFormCurrentPixColor();
        }

        public Command NavigateToAddNewKeyPageCommand => new Command(async () =>
        {
            await NavigateModalAsync(new AddPixKeyPage(this));
        });

        public Command ChangeSelectPixKeyCommand => new Command(async () =>
        {
            await SetStatusFormCurrentPixColor();
        });

        public Command SharePayloadCommand => new Command(async () =>
        {
            var options = new List<Acr.UserDialogs.ActionSheetOption>
            {
                new Acr.UserDialogs.ActionSheetOption("Compartilhar", async () =>
                {
                    await ShareText(CurrentPixKey?.Payload);
                }),
                new Acr.UserDialogs.ActionSheetOption("Copiar", async () =>
                {
                    await Clipboard.SetTextAsync(CurrentPixKey?.Payload);
                    DialogService.Toast("Código copiado com sucesso!");
                })
            };

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "O que deseja fazer?",
                //UseBottomSheet = true,
                Options = options,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        });

        public Command OpenOptionsCommand => new Command(() =>
        {
            var options = new List<Acr.UserDialogs.ActionSheetOption>
            {
                new Acr.UserDialogs.ActionSheetOption("Editar", async () =>
                {
                    await NavigateModalAsync(new AddPixKeyPage(this,CurrentPixKey));
                }),
                new Acr.UserDialogs.ActionSheetOption("Compartilhar chave", async () =>
                {
                    await ShareText(CurrentPixKey?.Key);
                }),
                new Acr.UserDialogs.ActionSheetOption("Copiar chave", async () =>
                {
                    await Clipboard.SetTextAsync(CurrentPixKey?.Key);
                    DialogService.Toast("Chave copiada com sucesso!");
                })
            };

            DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
            {
                Title = "O que deseja fazer?",
                //UseBottomSheet = true,
                Options = options,
                Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
                {
                    return;
                })
            });
        });

        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Escolha uma opção"
            });
        }

        private async Task SetStatusFormCurrentPixColor()
        {
            App.LoadTheme(CurrentPixKey.Color);
        }

        //private async Task LoadHideData()
        //{
        //    HideData = PreferenceService.HideData;
        //}

        private ObservableCollection<PixKey> _pixKeyList;
        public ObservableCollection<PixKey> PixKeyList
        {
            set => SetProperty(ref _pixKeyList, value);
            get => _pixKeyList;
        }

        private PixKey _currentPixKey;
        public PixKey CurrentPixKey
        {
            set => SetProperty(ref _currentPixKey, value);
            get => _currentPixKey;
        }

        private Payload _currentPayload;
        public Payload CurrentPayload
        {
            set => SetProperty(ref _currentPayload, value);
            get => _currentPayload;
        }

        private string _currentQrCode;
        public string CurrentQrCode
        {
            set => SetProperty(ref _currentQrCode, value);
            get => _currentQrCode;
        }

        private bool _hideData;
        public bool HideData
        {
            set => SetProperty(ref _hideData, value);
            get => _hideData;
        }

        ImageSource _qrCodeImage;
        public ImageSource QrCodeImage
        {
            get => _qrCodeImage;
            set => SetProperty(ref _qrCodeImage, value);
        }
    }
}
