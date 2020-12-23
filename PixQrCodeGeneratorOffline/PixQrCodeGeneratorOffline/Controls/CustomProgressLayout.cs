using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomProgressLayout : StackLayout
    {
        internal static int CurrentPhases { get; set; }

        public CustomProgressLayout()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Spacing = 3;
        }

        public static readonly BindableProperty PhasesProperty =
            BindableProperty.Create(nameof(Phases), typeof(int), typeof(CustomProgressLayout), 0,
                defaultBindingMode: BindingMode.Default,
                propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
                {
                    var value = (int)newValue;

                    if (value == 0)
                        return;

                    var b = (CustomProgressLayout)bindable;

                    CurrentPhases = value;

                    LoadPhases(b);
                });

        public int Phases
        {
            get { return (int)GetValue(PhasesProperty); }
            set { SetValue(PhasesProperty, value); }
        }

        public static readonly BindableProperty CurrentPhaseProperty =
            BindableProperty.Create(nameof(CurrentPhase), typeof(int), typeof(CustomProgressLayout), 0,
                defaultBindingMode: BindingMode.Default,
                propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
                {
                    var b = (CustomProgressLayout)bindable;

                    var value = (int)newValue;

                    if (value < 1)
                        return;

                    LoadPhases(b, value -= 1);
                });

        public int CurrentPhase
        {
            get { return (int)GetValue(CurrentPhaseProperty); }
            set { SetValue(CurrentPhaseProperty, value); }
        }

        public static readonly BindableProperty ExecuteOnFinishProgressCommandProperty =
           BindableProperty.Create(nameof(ExecuteOnFinishProgressCommand), typeof(ICommand), typeof(CustomProgressLayout), propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
           {
               var b = (CustomProgressLayout)bindable;
           });

        public ICommand ExecuteOnFinishProgressCommand
        {
            get { return (ICommand)GetValue(ExecuteOnFinishProgressCommandProperty); }
            set { SetValue(ExecuteOnFinishProgressCommandProperty, value); }
        }


        public static readonly BindableProperty ExecuteOnFinishProgressParameterCommandProperty =
           BindableProperty.Create(nameof(ExecuteOnFinishProgressParameterCommand), typeof(object), typeof(CustomProgressLayout), propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
           {
               var b = (CustomProgressLayout)bindable;
           });

        public object ExecuteOnFinishProgressParameterCommand
        {
            get { return (object)GetValue(ExecuteOnFinishProgressParameterCommandProperty); }
            set { SetValue(ExecuteOnFinishProgressParameterCommandProperty, value); }
        }

        private static void LoadPhases(BindableObject bindable, int phase = 0)
        {
            var b = (CustomProgressLayout)bindable;

            b.Children.Clear();

            for (int i = 0; i < CurrentPhases; i++)
            {
                var a = new CustomProgressLayoutAnimated();
                //a.WidthRequest = 2;

                b.Children.Add(a);

                if (b.Children.IndexOf(a) < phase)
                {
                    a.Progress = 1;
                    //a.BackgroundColor = Color.Red;
                }
            }

            var current = (b.Children[phase] as CustomProgressLayoutAnimated);

            Task.Run(async () =>
            {
                var a = 0.0;

                for (int i = 0; i <= 5; i++)
                {
                    await current.ProgressTo(a, 1000, Easing.Linear);
                    a += 0.20;
                }

                var index = b.Children.IndexOf(current);

                if (b.ExecuteOnFinishProgressCommand != null && index >= 0)
                    b.ExecuteOnFinishProgressCommand.Execute(b.ExecuteOnFinishProgressParameterCommand);
            });
        }
    }
}
