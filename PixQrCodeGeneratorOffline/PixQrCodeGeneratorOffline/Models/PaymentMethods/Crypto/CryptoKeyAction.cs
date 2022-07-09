using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto
{
    public class CryptoKeyAction : ActionBase
    {
        public static ObservableCollection<CryptoKeyAction> GetList()
        {
            return new ObservableCollection<CryptoKeyAction>
            {
                //new CryptoKeyAction
                //{
                //    Title = "Criar Cobrança",
                //    Icon = FontAwesomeSolid.HandHoldingUsd,
                //    //Command = pixKey?.Command?.NavigateToCreateBillingPageCommand,
                //    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                //},
                new CryptoKeyAction
                {
                    Title = "Copiar Chave",
                    Icon = FontAwesomeSolid.Copy,
                    Type = Pix.KeyActionType.CopyKey
                },
                new CryptoKeyAction
                {
                    Title = "Compartilhar Chave",
                    Icon = FontAwesomeSolid.ShareAlt,
                    Type = Pix.KeyActionType.ShareKey
                },
                new CryptoKeyAction
                {
                    Title = "Compartilhar no WhatsApp",
                    Icon = FontAwesomeBrands.Whatsapp,
                    Type = Pix.KeyActionType.ShareOnWhatsApp,
                    IconType = FontAwesomeType.brand
                },
                //new CryptoKeyAction
                //{
                //    Title = "Cobranças Salvas",
                //    Icon = FontAwesomeSolid.HandHoldingUsd,
                //    //Command = pixKey?.Command?.NavigateToBillingCommand,
                //    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                //},
                new CryptoKeyAction
                {
                    Title = "Ver Qr Code",
                    Icon = FontAwesomeSolid.Qrcode,
                    Type = Pix.KeyActionType.PaymentPage
                },
                //new PixKeyAction
                //{
                //    Title = "Baixar Qr Code",
                //    Icon = FontAwesomeSolid.Qrcode,
                //    Command = pixKey?.Command?.NavigateToDownloadQrCodeCommand,
                //    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                //},
                new CryptoKeyAction
                {
                    Title = "Editar Chave",
                    Icon = FontAwesomeSolid.Pen,
                    Type = Pix.KeyActionType.Edit
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
}
