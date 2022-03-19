using PixQrCodeGeneratorOffline.ViewModels;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Views
{
    public partial class StartMorePage : ContentPage
    {
        private readonly StartMoreViewModel moreViewModel;

        public StartMorePage()
        {
            InitializeComponent();

            BindingContext = moreViewModel = new StartMoreViewModel();
        }

        protected override void OnAppearing()
        {
            moreViewModel.LoadDataCommand.Execute(null);
        }
    }
}