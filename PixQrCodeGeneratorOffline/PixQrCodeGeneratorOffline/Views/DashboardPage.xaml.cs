using PixQrCodeGeneratorOffline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PixQrCodeGeneratorOffline.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        DashboardViewModel _viewModel;

        public DashboardPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new DashboardViewModel();
        }

        //private async void CustomEditor_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(e?.NewTextValue))
        //    {
        //        await _viewModel.CurrentPixKey.RaiseWithDelayPresentationAsync();
        //    }
        //}

        //protected override void OnAppearing()
        //{
        //    _viewModel.LoadDataCommand.Execute(null);
        //    base.OnAppearing();
        //}

        //private void CustomEntry_Unfocused(object sender, FocusEventArgs e)
        //{
        //    _viewModel.CurrentPixKey.RaisePresentation();
        //}

        //private void CustomSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    _viewModel.CurrentPixKey.Value = e.NewValue.ToString();
        //}
    }
}