using PixQrCodeGeneratorOffline.Extention;
using System;
using System.Collections.Generic;
using System.Text;
using static PixQrCodeGeneratorOffline.Extention.IconExtention;

namespace PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix
{
    public class PixKeyAction
    {
        public string Icon { get; set; }

        public string Title { get; set; }

        public System.Windows.Input.ICommand Command { get; set; }

        public MaterialColor Colors { get; set; }

        public FontAwesomeType IconType { get; set; } = FontAwesomeType.solid;

        public static List<PixKeyAction> GetList(PixKey pixKey)
        {
            return new List<PixKeyAction>
            {
                new PixKeyAction
                {
                    Title = "Criar Cobrança",
                    Icon = FontAwesomeSolid.HandHoldingUsd,
                    Command = pixKey?.Command?.NavigateToCreateBillingPageCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Copiar Chave",
                    Icon = FontAwesomeSolid.Copy,
                    Command = pixKey?.Command?.CopyKeyCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Compartilhar Chave",
                    Icon = FontAwesomeSolid.ShareAlt,
                    Command = pixKey?.Command?.ShareKeyCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
                {
                    Title = "Compartilhar no WhatsApp",
                    Icon = FontAwesomeBrands.Whatsapp,
                    Command = pixKey?.Command?.ShareOnWhatsCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor,
                    IconType = FontAwesomeType.brand
                },
                new PixKeyAction
                {
                    Title = "Cobranças Salvas",
                    Icon = FontAwesomeSolid.HandHoldingUsd,
                    Command = pixKey?.Command?.NavigateToBillingCommand,
                    Colors = pixKey?.FinancialInstitution?.Institution?.MaterialColor
                },
                new PixKeyAction
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
                new PixKeyAction
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
