using System.Reflection;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class ImageLocal : Image
    {
        public static readonly BindableProperty LocalPathProperty =
          BindableProperty.Create(propertyName: nameof(LocalPath),
                              returnType: typeof(string),
                              declaringType: typeof(ImageLocal),
                              propertyChanged: LocalPathPropertyChanged);

        public string LocalPath
        {
            get => (string)GetValue(LocalPathProperty);
            set => SetValue(LocalPathProperty, value);
        }

        static void LocalPathPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ImageLocal control && newValue is string value && !string.IsNullOrWhiteSpace(value))
                control.Source = ImageSource.FromResource($"PixQrCodeGeneratorOffline.{value}", typeof(ImageLocal).GetTypeInfo().Assembly);
        }
    }
}
