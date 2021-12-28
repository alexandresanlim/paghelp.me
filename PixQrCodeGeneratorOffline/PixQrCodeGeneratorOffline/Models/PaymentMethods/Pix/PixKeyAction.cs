using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix
{
    public class PixKeyAction : ActionBase
    {
        public static ObservableCollection<PixKeyAction> GetList()
        {
            return new ObservableCollection<PixKeyAction>
            {
                new PixKeyAction
                {
                    Title = "Criar Cobrança Estática",
                    Icon = FontAwesomeSolid.HandHoldingUsd,
                    Type = KeyActionType.CreateBilling,
                    //Command = command
                    //Command = pixKey?.Command?.NavigateToCreateBillingPageCommand,
                    //Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Copiar Chave",
                    Icon = FontAwesomeSolid.Copy,
                    Type = KeyActionType.CopyKey
                    //Command = pixKey?.Command?.CopyKeyCommand,
                    //Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Compartilhar Chave",
                    Icon = FontAwesomeSolid.ShareAlt,
                    Type = KeyActionType.ShareKey
                    //Command = pixKey?.Command?.ShareKeyCommand,
                    //Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Compartilhar no WhatsApp",
                    Icon = FontAwesomeBrands.Whatsapp,
                    Type = KeyActionType.ShareOnWhatsApp,
                    //Command = pixKey?.Command?.ShareOnWhatsCommand,
                    //Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor,
                    IconType = FontAwesomeType.brand
                },
                new PixKeyAction
                {
                    Title = "Cobranças Salvas",
                    Icon = FontAwesomeSolid.HandHoldingUsd,
                    Type = KeyActionType.BillingList,
                    //Command = pixKey?.Command?.NavigateToBillingCommand,
                    //Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Ver Qr Code",
                    Icon = FontAwesomeSolid.Qrcode,
                    Type = KeyActionType.PaymentPage,
                    //Command = pixKey?.Command?.NavigateToPaymentPageCommand,
                    //Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                //new PixKeyAction
                //{
                //    Title = "Baixar Qr Code",
                //    Icon = FontAwesomeSolid.Qrcode,
                //    Command = pixKey?.Command?.NavigateToDownloadQrCodeCommand,
                //    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                //},
                new PixKeyAction
                {
                    Title = "Editar Chave",
                    Icon = FontAwesomeSolid.Pen,
                    Type = KeyActionType.Edit
                    //Command = pixKey?.Command?.EditKeyCommand,
                    //Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                
                //new PixKeyAction
                //{
                //    Title = "Excluir",
                //    Icon = FontAwesomeSolid.TrashAlt,
                //    Command = pixKey?.Command?.EditKeyCommand,
                //    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                //}
            };
        }
    }

    public enum KeyActionType
    {
        None,
        CreateBilling,
        CopyKey,
        ShareKey,
        ShareOnWhatsApp,
        BillingList,
        PaymentPage,
        Edit
    }

}
