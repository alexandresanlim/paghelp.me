using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public partial class CustomProgress : StackLayout
    {
        public CustomProgress()
        {
            Spacing = 0;
            //Children.Add(AddText());
            Children.Add(AddPhase());
        }

        private static StackLayout CurrentChild { get; set; }

        //private static Label CurrentTextPhase { get; set; }

        private StackLayout AddPhase()
        {
            CurrentChild = new StackLayout
            {
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
            };

            return CurrentChild;
        }

        //private Label AddText()
        //{
        //    CurrentTextPhase = new Label { FontAttributes = FontAttributes.Bold };
        //    return CurrentTextPhase;
        //}

        public static readonly BindableProperty PhasesProperty =
            BindableProperty.Create(nameof(Phases), typeof(int), typeof(CustomProgress), 0,
                defaultBindingMode: BindingMode.Default,
                propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
                {
                    if ((int)newValue == 0)
                        return;

                    CurrentChild.Children.Clear();

                    for (int i = 0; i < (int)newValue; i++)
                    {
                        CurrentChild.Children.Add(new BoxView
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = ColorLessValue,
                            HeightRequest = 3
                        });
                    }

                    //CurrentTextPhase.Text = "0 / " + CurrentChild.Children.Count.ToString();
                });

        public static readonly BindableProperty CurrentPhaseProperty =
            BindableProperty.Create(nameof(CurrentPhase), typeof(int), typeof(CustomProgress), 0,
                defaultBindingMode: BindingMode.Default,
                propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
                {

                    var positive = CurrentChild.Children.Take((int)newValue);

                    foreach (var item in positive)
                    {
                        item.BackgroundColor = ColorMoreValue;
                    }

                    var negative = CurrentChild.Children.Where(x => !positive.Contains(x));

                    foreach (var item in negative)
                    {
                        item.BackgroundColor = ColorLessValue;
                    }

                    //CurrentTextPhase.Text = positive.Count().ToString() + " / " + CurrentChild.Children.Count.ToString();

                });

        public static readonly BindableProperty ColorMoreProperty =
           BindableProperty.Create(nameof(ColorMore), typeof(Color), typeof(CustomProgress), Color.DarkGray,
               defaultBindingMode: BindingMode.Default,
               propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
               {
                   ColorMoreValue = (Color)newValue;
               });

        public static readonly BindableProperty ColorLessProperty =
          BindableProperty.Create(nameof(ColorLess), typeof(Color), typeof(CustomProgress), Color.LightGray,
              defaultBindingMode: BindingMode.Default,
              propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
              {
                  ColorLessValue = (Color)newValue;
              });

        public int Phases
        {
            get { return (int)GetValue(PhasesProperty); }
            set { SetValue(PhasesProperty, value); }
        }

        public int CurrentPhase
        {
            get { return (int)GetValue(CurrentPhaseProperty); }
            set { SetValue(CurrentPhaseProperty, value); }
        }

        public Color ColorMore
        {
            get { return (Color)GetValue(ColorMoreProperty); }
            set { SetValue(ColorMoreProperty, value); }
        }

        public Color ColorLess
        {
            get { return (Color)GetValue(ColorMoreProperty); }
            set { SetValue(ColorLessProperty, value); }
        }

        //public static Color ColorMore => Color.DarkGray;

        private static Color ColorMoreValue { get; set; }

        private static Color ColorLessValue { get; set; }
    }
}
