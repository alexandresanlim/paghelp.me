﻿using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Controls
{
    public class CustomCollectionView : CollectionView
    {
        public CustomCollectionView()
        {
            PropertyChanged += CollectionView_PropertyChanged;
        }

        private void CollectionView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is CollectionView c && e.PropertyName.Equals(nameof(CollectionView.ItemsSource)))
            {
                Task.Run(async () =>
                {
                    c.Opacity = 0;
                    await c.FadeTo(1, 500);
                });
            }
        }
    }
}
