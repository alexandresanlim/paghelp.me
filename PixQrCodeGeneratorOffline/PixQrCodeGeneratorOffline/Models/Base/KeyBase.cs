using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Base
{
    public class KeyBase : NotifyObjectBase
    {
        [LiteDB.BsonId]
        public int Id { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsContact { get; set; }

        private string _key;
        public string Key
        {
            set { SetProperty(ref _key, value); }
            get { return _key; }
        }
    }
}
