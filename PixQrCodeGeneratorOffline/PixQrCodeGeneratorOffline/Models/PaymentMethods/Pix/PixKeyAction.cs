using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Base;
using System.Collections.ObjectModel;
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
                },
                new PixKeyAction
                {
                    Title = "Copiar Chave",
                    Icon = FontAwesomeSolid.Copy,
                    Type = KeyActionType.CopyKey
                },
                new PixKeyAction
                {
                    Title = "Compartilhar Chave",
                    Icon = FontAwesomeSolid.ShareAlt,
                    Type = KeyActionType.ShareKey
                },
                new PixKeyAction
                {
                    Title = "Compartilhar no WhatsApp",
                    Icon = FontAwesomeBrands.Whatsapp,
                    Type = KeyActionType.ShareOnWhatsApp,
                    IconType = FontAwesomeType.brand
                },
                new PixKeyAction
                {
                    Title = "Cobranças Salvas",
                    Icon = FontAwesomeSolid.HandHoldingUsd,
                    Type = KeyActionType.BillingList,
                },
                new PixKeyAction
                {
                    Title = "Ver Qr Code",
                    Icon = FontAwesomeSolid.Qrcode,
                    Type = KeyActionType.PaymentPage,
                },
                new PixKeyAction
                {
                    Title = "Baixar Qr Code",
                    Icon = FontAwesomeSolid.Download,
                    Type= KeyActionType.DownloadQRCode,
                    RequiresInternet = true
                    
                },
                new PixKeyAction
                {
                    Title = "Editar Chave",
                    Icon = FontAwesomeSolid.Pen,
                    Type = KeyActionType.Edit
                },
                new PixKeyAction
                {
                    Title = "Excluir Chave",
                    Icon = FontAwesomeSolid.Trash,
                    Type = KeyActionType.Delete
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
        Edit,
        DownloadQRCode,
        Delete
    }

}
