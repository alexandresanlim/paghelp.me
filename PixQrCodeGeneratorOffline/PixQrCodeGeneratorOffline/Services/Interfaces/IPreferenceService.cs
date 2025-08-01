﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Services.Interfaces
{
    public interface IPreferenceService
    {
        void ChangeHideData();

        void ChangeShowInList();

        Task<bool> ChangeFingerPrint();

        void ChangePDVMode();

        void ChangeShowNewsMode();

        void ChangeTheme();

        void ChangeCrypto();

        void ChangeAreYouLikingAppMsgCount();

        void ChangeLikingAppMsgWasShowed(bool value);

        Task RequireAuthenticationToAction(Action execute, bool checkPreference = true);
    }
}
