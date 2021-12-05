using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline
{
    public class SnippetTests : Frame
    {

        public static readonly BindableProperty MyPropertyProperty =
           BindableProperty.Create(
               propertyName: nameof(MyProperty),
               returnType: typeof(string),
               declaringType: typeof(object),
               defaultValue: string.Empty,
               defaultBindingMode: BindingMode.Default,
               validateValue: null,
               propertyChanged: MyPropertyPropertyChanged);

        public string MyProperty
        {
            get => (string)GetValue(MyPropertyProperty);
            set => SetValue(MyPropertyProperty, value);
        }

        static void MyPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
                return;

            var template = (object)bindable;
            string value = (string)newValue;
        }
    }
}
