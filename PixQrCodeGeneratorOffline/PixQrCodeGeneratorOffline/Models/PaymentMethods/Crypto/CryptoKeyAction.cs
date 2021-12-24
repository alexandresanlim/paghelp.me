using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using System.Collections.Generic;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto
{
    public class CryptoKeyAction : ActionBase
    {
        public static List<CryptoKeyAction> GetList(CryptoKey pixKey)
        {
            return new List<CryptoKeyAction>
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
                    Command = pixKey?.Command?.CopyKeyCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new CryptoKeyAction
                {
                    Title = "Compartilhar Chave",
                    Icon = FontAwesomeSolid.ShareAlt,
                    Command = pixKey?.Command?.ShareKeyCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new CryptoKeyAction
                {
                    Title = "Compartilhar no WhatsApp",
                    Icon = FontAwesomeBrands.Whatsapp,
                    Command = pixKey?.Command?.ShareOnWhatsCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor,
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
                    Command = pixKey?.Command?.NavigateToPaymentPageCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
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
                    Command = pixKey?.Command?.EditKeyCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
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
