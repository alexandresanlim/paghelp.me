using Acr.UserDialogs;
using Microsoft.AppCenter.Crashes;
using System;

namespace PixQrCodeGeneratorOffline.Extention
{
    public static class ExceptionExtention
    {
        public static void SendToLog(this Exception exception)
        {
            Crashes.TrackError(exception);
            UserDialogs.Instance.Toast("Ops! Algo de errado aconteceu, uma mensagem foi enviada aos desenvolvedores.");
        }
    }
}
