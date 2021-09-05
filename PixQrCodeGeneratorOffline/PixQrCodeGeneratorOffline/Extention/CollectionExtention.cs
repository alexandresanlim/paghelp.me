using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PixQrCodeGeneratorOffline.Extention
{
    public static class CollectionExtention
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }
    }
}
