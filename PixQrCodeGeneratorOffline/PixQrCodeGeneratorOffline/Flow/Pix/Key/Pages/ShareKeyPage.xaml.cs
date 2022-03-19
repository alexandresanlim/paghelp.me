using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using PixQrCodeGeneratorOffline.ViewModels;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class ShareKeyPage : ContentPage
    {
        private readonly ShareKeyViewModel _shareKeyViewModel;

        private static ShareKeyLoadDataParameter _shareKeyLoadDataParameter;

        public ShareKeyPage(ObservableCollection<PixKey> pixKeyList)
        {
            InitializeComponent();

            BindingContext = _shareKeyViewModel = new ShareKeyViewModel(pixKeyList);
            _shareKeyLoadDataParameter = new ShareKeyLoadDataParameter();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var option = (Switch)sender;

            switch (option.ClassId)
            {
                case "AddInst":
                    _shareKeyLoadDataParameter.Institution = e.Value;
                    break;

                case "Point":
                    _shareKeyLoadDataParameter.Point = e.Value;
                    break;

                case "SameLine":
                    _shareKeyLoadDataParameter.SameLine = e.Value;
                    break;

                case "AddSeparator":
                    _shareKeyLoadDataParameter.AddSeparator = e.Value;
                    break;

                case "AddDescription":
                    _shareKeyLoadDataParameter.AddDescription = e.Value;
                    break;

                default:
                    break;
            }

            _shareKeyViewModel.LoadData(_shareKeyLoadDataParameter);
        }
    }
}