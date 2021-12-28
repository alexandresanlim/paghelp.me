using PixQrCodeGeneratorOffline.Extention;
using System.Collections.ObjectModel;

namespace PixQrCodeGeneratorOffline.ViewModels.Helpers
{
    public class DashboardViewModelHelper
    {
    }

    public class DashboardWelcome
    {
        public string Emoji { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Unconnection { get; set; }

        public static ObservableCollection<DashboardWelcome> GetList()
        {
            return new ObservableCollection<DashboardWelcome>
            {
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.Plane,
                    Title = "Offline",
                    Description = "Não preocupa-se com a sua conexão, aqui a maioria das funcionalidades são offline (sem conexão com a internet).",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.Lock,
                    Title = "Seguro",
                    Description = "Guarde suas chaves localmente de maneira criptografada e sem conexão com a internet, com suporte a autenticação biométrica se disponível pelo seu aparelho.",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.HandHoldingUsd,
                    Title = "Cobranças",
                    Description = "Gere Qr Codes para pagamento.",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.ThumbsUp,
                    Title = "Prático",
                    Description = "Compartilhe uma única ou todas suas chaves rapidamente, incluindo com geração de txt",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.Cogs,
                    Title = "Customizável",
                    Description = "Suporte a dark e light mode",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.Save,
                    Title = "Backup",
                    Description = "Local, automático e criptografado.",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.ShieldAlt,
                    Title = "Anti-Roubo",
                    Description = "Você não precisa mais carregar com você o app do seu banco para gerar cobranças, previnindo-se de quadrilhas e assaltos.",
                    Unconnection = true
                },
                new DashboardWelcome
                {
                    Emoji = FontAwesomeSolid.ExclamationTriangle,
                    Title = "IMPORTANTE!",
                    Description = "- Para sua segurança, não fazemos conexão direta com o seu banco, sendo assim não será possível ver saldo ou retirar valores do seu banco, para isso use o app oficial do mesmo e jamais forneça esse tipo de acesso para terceiros. \n\n"
                }
            };
        }
    }
}
