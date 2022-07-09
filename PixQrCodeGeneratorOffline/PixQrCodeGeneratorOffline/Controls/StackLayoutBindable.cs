using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class StackLayoutBindable : StackLayout
    {
        public static readonly BindableProperty ItemsProperty =
       BindableProperty.Create(nameof(Items), typeof(ObservableCollection<View>), typeof(StackLayoutBindable), null,
           propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
           {

               if (newValue == null)
                   return;

               var b = (bindable as StackLayoutBindable);

               var viewAdd = (ObservableCollection<View>)newValue;

               foreach (var item in viewAdd)
               {
                   b.Children.Add(item);
               }


               //b.chi

               //(n as ObservableCollection<View>).CollectionChanged += (coll, arg) =>
               //{
               //    switch (arg.Action)
               //    {
               //        case NotifyCollectionChangedAction.Add:
               //            foreach (var v in arg.NewItems)
               //                (b as StackLayoutBindable).Children.Add((View)v);
               //            break;
               //        case NotifyCollectionChangedAction.Remove:
               //            foreach (var v in arg.NewItems)
               //                (b as StackLayoutBindable).Children.Remove((View)v);
               //            break;
               //        case NotifyCollectionChangedAction.Reset:
               //            (b as StackLayoutBindable).Children.Clear();
               //            break;
               //        case NotifyCollectionChangedAction.Move:
               //            //Do your stuff
               //            break;
               //        case NotifyCollectionChangedAction.Replace:
               //            //Do your stuff
               //            break;
               //    }
               //};
           });


        public ObservableCollection<View> Items
        {
            get { return (ObservableCollection<View>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
    }
}
