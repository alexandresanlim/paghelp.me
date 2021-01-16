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
using System.Windows.Input;
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

        public ICommand LoadDataCommand => new Command(async () =>
        {
            var list = PixKeyDataBase.GetAll();

            PixKeyList = list.ToObservableCollection();

            await LoadCurrentPixKey();

            await SetStatusFromCurrentPixColor();
        });

        public async Task LoadCurrentPixKey(PixKey pixKeySelected = null)
       {
            if (PixKeyList == null || !(PixKeyList.Count > 0))
            {
                ShowWelcome = true;

                CurrentPixKey = new PixKey
                {
                    Color = MaterialColor.GetByCurrentResourceThemeColor()
                };
            }

            else
            { 
                CurrentPixKey = pixKeySelected ?? PixKeyList.FirstOrDefault();
                ShowWelcome = false;
            }
        }

        public ICommand NavigateToCreateBillingPageCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(1000);

                await NavigateModalAsync(new CreateBillingPage(CurrentPixKey));
            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {
                SetIsLoading(false);
            }
        });

        public ICommand NavigateToAddNewKeyPageCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(1000);

                await NavigateModalAsync(new AddPixKeyPage(this));
            }
            catch (System.Exception)
            {

                throw;
            }
            finally
            {
                SetIsLoading(false);
            }
        });

        public ICommand ChangeSelectPixKeyCommand => new Command(async () =>
        {
            await SetStatusFromCurrentPixColor();
        });

        public ICommand CopyKeyCommand => new Command(async () =>
        {
            await Clipboard.SetTextAsync(CurrentPixKey?.Key);
            DialogService.Toast("Chave copiada com sucesso!");
        });

        public ICommand ShareKeyCommand => new Command(async () =>
        {
            try
            {
                SetIsLoading(true);

                await Task.Delay(1000);

                await ShareText(CurrentPixKey?.Key);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                SetIsLoading(false);
            }
        });

        public ICommand EditKeyCommand => new Command(async () =>
        {
            await NavigateModalAsync(new AddPixKeyPage(this, CurrentPixKey));
        });

        //public ICommand OpenOptionsCommand => new Command(() =>
        //{
        //    var options = new List<Acr.UserDialogs.ActionSheetOption>
        //    {
        //        new Acr.UserDialogs.ActionSheetOption("Editar chave", async () =>
        //        {
        //            await NavigateModalAsync(new AddPixKeyPage(this,CurrentPixKey));
        //        }),
        //        new Acr.UserDialogs.ActionSheetOption("Copiar chave", async () =>
        //        {
        //            await Clipboard.SetTextAsync(CurrentPixKey?.Key);
        //            DialogService.Toast("Chave copiada com sucesso!");
        //        }),
        //        new Acr.UserDialogs.ActionSheetOption("Compartilhar chave", async () =>
        //        {
        //            await ShareText(CurrentPixKey?.Key);
        //        })
        //    };

        //    DialogService.ActionSheet(new Acr.UserDialogs.ActionSheetConfig
        //    {
        //        Title = "O que deseja fazer?",
        //        //UseBottomSheet = true,
        //        Options = options,
        //        Cancel = new Acr.UserDialogs.ActionSheetOption("Cancelar", () =>
        //        {
        //            return;
        //        })
        //    });
        //});

        public async Task SetStatusFromCurrentPixColor()
        {
            if (CurrentPixKey?.Color == null)
                return;

            App.LoadTheme(CurrentPixKey?.Color);
        }

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

        private bool _showWelcome;
        public bool ShowWelcome
        {
            set => SetProperty(ref _showWelcome, value);
            get => _showWelcome;
        }

        //ImageSource _qrCodeImage;
        //public ImageSource QrCodeImage
        //{
        //    get => _qrCodeImage;
        //    set => SetProperty(ref _qrCodeImage, value);
        //}
    }
}
