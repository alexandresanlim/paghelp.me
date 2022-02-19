using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PixQrCodeGeneratorOffline.Extention
{
    public static class CollectionExtention
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return true;

            return !collection.Any();
        }
    }
}
