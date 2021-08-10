using PixQrCodeGeneratorOffline.Behavior;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomFrameButton : Frame
    {
        private CustomLabel TexButtonValue { get; set; } = new CustomLabel
        {
            FontAttributes = FontAttributes.Bold,
            TextColor = App.ThemeColors.TextOnPrimary,
            IsVisible = false
        };

        private TapViewBehavior TapButtonValue { get; set; } = new TapViewBehavior();

        private CustomIcon IconValue { get; set; } = new CustomIcon
        {
            IsVisible = false
        };

        private Thickness PaddingValue { get; set; } = new Thickness(13);

        public CustomFrameButton()
        {
            BackgroundColor = App.ThemeColors.Primary;
            CornerRadius = 25;
            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = PaddingValue,
                Children =
                {
                    IconValue,
                    TexButtonValue
                    //AddText(),
                }
            };
            Behaviors.Add(TapButtonValue);
            Padding = new Thickness(0);
        }

        public static readonly BindableProperty TextButtonProperty =
            BindableProperty.Create(nameof(TextButton), typeof(string), typeof(CustomFrameButton), null, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                var value = (string)newValue;

                if (string.IsNullOrEmpty(value))
                    return;

                var b = (CustomFrameButton)bindable;

                b.TexButtonValue.Text = value;
                b.TexButtonValue.IsVisible = true;
            });

        public string TextButton
        {
            get { return (string)GetValue(TextButtonProperty); }
            set { SetValue(TextButtonProperty, value); }
        }

        public static readonly BindableProperty TapButtonCommandProperty =
            BindableProperty.Create(nameof(TapButtonCommand), typeof(ICommand), typeof(CustomFrameButton), propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                var b = (CustomFrameButton)bindable;
                b.TapButtonValue.Command = (ICommand)newValue;
            });

        public ICommand TapButtonCommand
        {
            get { return (ICommand)GetValue(TapButtonCommandProperty); }
            set { SetValue(TapButtonCommandProperty, value); }
        }


        public static readonly BindableProperty TapButtonCommandParameterProperty =
            BindableProperty.Create(nameof(TapButtonCommandParameter), typeof(object), typeof(CustomFrameButton), propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                var b = (CustomFrameButton)bindable;
                b.TapButtonValue.CommandParameter = (object)newValue;
            });

        public object TapButtonCommandParameter
        {
            get { return (object)GetValue(TapButtonCommandParameterProperty); }
            set { SetValue(TapButtonCommandParameterProperty, value); }
        }

        public static readonly BindableProperty LeftIconProperty =
            BindableProperty.Create(nameof(LeftIcon), typeof(string), typeof(CustomFrameButton), propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                var b = (CustomFrameButton)bindable;

                b.IconValue.Glyph = (string)newValue;
                b.IconValue.IsVisible = true;
            });

        public string LeftIcon
        {
            get { return (string)GetValue(LeftIconProperty); }
            set { SetValue(LeftIconProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
           BindableProperty.Create(nameof(FontSize), typeof(NamedSize), typeof(CustomFrameButton), NamedSize.Default, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
           {
               var value = (NamedSize)newValue;

               var b = (CustomFrameButton)bindable;

               b.TexButtonValue.FontSize = Device.GetNamedSize(value, typeof(Label));
               b.IconValue.IconSize = value == NamedSize.Micro ? NamedSize.Small : value == NamedSize.Small ? NamedSize.Medium : value == NamedSize.Medium ? NamedSize.Large : NamedSize.Default;
           });

        public NamedSize FontSize
        {
            get { return (NamedSize)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty ButtonStyleProperty =
           BindableProperty.Create(nameof(ButtonStyle), typeof(CustomFrameButtonStyle), typeof(CustomFrameButton), CustomFrameButtonStyle.Modern, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
           {
               var control = (CustomFrameButton)bindable;
               var value = (CustomFrameButtonStyle)newValue;

               switch (value)
               {
                   case CustomFrameButtonStyle.Round:
                       control.CornerRadius = 25;
                       break;

                   case CustomFrameButtonStyle.Modern:
                   default:
                       control.CornerRadius = 25;
                       break;
               }
           });

        public CustomFrameButtonStyle ButtonStyle
        {
            get { return (CustomFrameButtonStyle)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }
    }

    public enum CustomFrameButtonStyle
    {
        Modern,
        Round
    }
}
