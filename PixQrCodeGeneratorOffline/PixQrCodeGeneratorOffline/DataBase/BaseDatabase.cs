using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PixQrCodeGeneratorOffline.DataBase
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
                    _dataBase = new LiteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.AppName.Replace(" ", "").ToLower() + ".db"));
                }

                return _dataBase;
            }
        }
    }
}
