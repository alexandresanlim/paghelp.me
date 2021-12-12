﻿using AsyncAwaitBestPractices.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Behavior
{
    public class TapViewBehavior : Behavior<View>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(IAsyncCommand), typeof(TapViewBehavior), null, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                var control = (TapViewBehavior)bindable;
                control.Command = (AsyncCommand)newValue;
            });

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(TapViewBehavior), null, BindingMode.Default, null, propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                var control = (TapViewBehavior)bindable;
                control.CommandParameter = (object)newValue;
            });

        public TapViewBehavior()
        {
            Initialize();
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public View AssociatedObject { get; private set; }

        private ICommand TransitionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    AssociatedObject.AnchorX = 0.48;
                    AssociatedObject.AnchorY = 0.48;
                    await AssociatedObject.ScaleTo(0.8, 50, Easing.Linear);
                    await Task.Delay(100);
                    await AssociatedObject.ScaleTo(1, 50, Easing.Linear);
                    if (Command != null)
                    {
                        Command.Execute(CommandParameter);
                    }
                });
            }
        }

        public void Initialize()
        {
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable as View;

            AssociatedObject?.GestureRecognizers?.Add(new TapGestureRecognizer()
            {
                Command = TransitionCommand
            });
        }


        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
        }
    }
}
