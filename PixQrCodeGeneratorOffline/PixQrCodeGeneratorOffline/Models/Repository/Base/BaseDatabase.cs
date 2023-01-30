using LiteDB;
using System;
using System.IO;

namespace PixQrCodeGeneratorOffline.Models.Repository.Base
{
    public abstract class BaseDatabase
    {
        private static LiteDatabase _dataBase;

        public static LiteDatabase GetDatabase
        {
            get
            {
                if (_dataBase == null)
                {
                    _dataBase = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DataBaseName));
                }

                return _dataBase;
            }
        }
    }
}
