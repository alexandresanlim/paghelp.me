using PixQrCodeGeneratorOffline.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}