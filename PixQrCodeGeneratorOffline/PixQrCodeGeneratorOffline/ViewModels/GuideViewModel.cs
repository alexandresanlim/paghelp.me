using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels
{
    public class GuideViewModel : BaseViewModel
    {
        private readonly IGuideService _guideService;

        public GuideViewModel()
        {
            _guideService = DependencyService.Get<IGuideService>();

            LoadData.Execute(null);
        }

        public ICommand LoadData => new Command(() =>
        {
            try
            {
                GuideList = new ObservableCollection<Guide>
                {
                    _guideService.Create("O que é o PIX?",
                    "O Pix é a solução de pagamento instantâneo, criada e gerida pelo Banco Central do Brasil(BC), que proporciona a realização de transferências e de pagamentos.\n\n" +
                    "O Pix é concluído em poucos segundos, inclusive em relação à disponibilização dos recursos para o recebedor."),

                    _guideService.Create("O PIX é um meio de pagamento?",
                    "Sim. O Pix é um meio de pagamento assim como boleto, TED, DOC, transferências entre contas de uma mesma instituição e cartões de pagamento (débito, crédito e pré-pago). \n\n" +
                    "A diferença é que o Pix permite que qualquer tipo de transferência e de pagamento seja realizada em qualquer dia, incluindo fins de semana e feriados, e em qualquer hora. \n\n" +
                    "Veja na seção 2 desta FAQ as diferenças entre o Pix e os demais meios de pagamento."),

                    _guideService.Create("Onde posso acessar o PIX?", "O acesso ao Pix ocorre exclusivamente pelos canais de atendimento das instituições financeiras e de pagamento (celular, internet banking, agências, caixas eletrônicos) ou nos correspondentes bancários, como lotéricas, por exemplo. " + "O Pix é um meio de pagamento disponibilizado por esses prestadores de serviço. Para evitar golpes, tenha a certeza de que está acessando um dos canais autorizados pelo seu banco ou instituição. \n\n" + "Não acesse links de sites falsos."),

                    _guideService.Create("O PIX já está em funcionamento?", "Sim. O Pix está em funcionamento pleno desde 16 de novembro de 2020. \n\n" + "Consulte se seu banco ou instituição de pagamento oferece esse meio de pagamento. \n\n" + "Importante! O registro das chaves já podia ser realizado desde 5 de outubro de 2020. \n\n" + "Além disso, de 3 a 15 de novembro, ocorreu a operação restrita do Pix, apenas com alguns clientes pré-selecionados pelas instituições."),

                    _guideService.Create("O PIX usa blockchain?",
                    "​Não. O Pix usa estrutura tecnológica centralizada, na qual a comunicação entre os diversos participantes e o BC é realizada por meio de mensageria."),

                    _guideService.Create("O PIX é seguro?", "A segurança faz parte do desenho do Pix desde seu princípio, e é priorizada em todos os aspectos do ecossistema, inclusive em relação às transações, às informações pessoais e ao combate à fraude e lavagem de dinheiro. \n\n" + "Os requisitos de disponibilidade, confidencialidade, integridade e autenticidade das informações foram cuidadosamente estudados e diversos controles foram implantados para garantir alto nível de segurança. \n\n" + "Todas as transações ocorrerão por meio de mensagens assinadas digitalmente e que trafegam de forma criptografada, em uma rede protegida e apartada da Internet. Além disso, No Diretório de Identificadores de Contas Transacionais (DICT), componente que armazenará as informações das chaves Pix, as informações dos usuários também são criptografadas e existem mecanismos de proteção que impedem varreduras das informações pessoais, além de indicadores que auxiliam os participantes do ecossistema na prevenção contra fraudes e lavagem de dinheiro."),

                    _guideService.Create("Como eu posso ter acesso ao regulamento do PIX?", "​​O Regulamento do Pix abrange a Resolução BCB Nº 1, de 2020, que disciplina seu funcionamento. \n\n" + "Na página do Pix no site do Banco Central, é possível encontrar a relação de manuais do Pix, no menu à direita, em 'Regulamentação relacionada ao Pix'."),

                    _guideService.Create("O que é uma chave PIX?", "A chave é um 'apelido' utilizado para identificar sua conta. Ela representa o endereço da sua conta no Pix. \n\n" + "Os quatro tipos de chaves Pix que você pode utilizar são: CPF/CNPJ; E-mail; Número de telefone celular; ou Chave aleatória. A chave vincula uma dessas informações básicas às informações completas que identificam a conta transacional do cliente (identificação da instituição financeira ou de pagamento, número da agência, número da conta e tipo de conta)."),

                    _guideService.Create("O que é uma chave aleatória?", "A chave aleatória é uma forma de você receber um Pix sem precisar informar quaisquer dados pessoais ao pagador. \n\n" + "É um código único, de 32 caracteres com letras e símbolos, gerado aleatoriamente pelo Banco Central e atrelado a uma única conta. Essa opção foi criada principalmente para ser utilizada com QR codes gerados por meio do aplicativo de sua instituição, a fim de facilitar o recebimento de recursos financeiros. \n\n" + "Ela também pode ser copiada e enviada, por exemplo, por mensagem, não sendo a intenção que seja memorizada pelo usuário. O usuário pode cadastrar múltiplas chaves aleatórias, seja vinculada à mesma conta ou a contas diferentes, desde que dentro do limite de 5 chaves por conta, se pessoa física, e 20 chaves por conta se pessoa jurídica. Esse é o único tipo de chave que não é possível realizar a portabilidade. \n\n" + "Assim, basta simplesmente excluir na conta origem e cadastrar uma nova chave aleatória na conta destino.​"),

                    _guideService.Create("Sou obrigado a cadastrar uma chave PIX para poder utilizar o PIX?", "Não é necessário cadastrar uma chave para fazer ou receber um Pix. \n\n" + "No entanto, o cadastramento da chave é altamente recomendável para receber um Pix. \n\n" + "Ainda que você possa receber transações apenas informando os dados da sua conta, essa forma não tem a mesma praticidade que o uso da chave possibilita e pode gerar demora na iniciação da transação, diminuindo o benefício do pagador em fazer um Pix."),

                    _guideService.Create("Quantas chaves eu posso ter?", "​Cada conta de pessoa física pode ter até 5 chaves vinculadas à ela, independentemente da quantidade de titulares. \n\n" + "Ou seja, se a conta for individual ou conjunta, ela poderá ter, no máximo, 5 chaves Pix. \n\n" + "Já no caso de pessoa jurídica, o máximo é de 20 chaves por conta."),

                    _guideService.Create("Como efetuar os registros das minhas chaves no PIX?", "Você poderá realizar o registro das suas chaves por meio de um dos canais de acesso da instituição em que você possui conta (inclusive aplicativo instalado em seu smartphone). \n\n" + "Para realizar o registro, você precisará confirmar a posse da chave e vinculá-la a uma conta para recebimento dos recursos. Para confirmação da posse da chave, sua instituição enviará um código por SMS para o número de telefone celular que você quer utilizar como chave (ou para o e-mail que se quer utilizar como chave, se for o caso). \n\n" + "Esse código deverá ser inserido no canal de acesso disponibilizado por sua instituição financeira ou de pagamento, mediante autenticação digital apropriada, como solicitação de senha, biometria ou reconhecimento facial, por exemplo. Atenção! A confirmação não pode ser efetivada por contato telefônico nem por link enviado por meio de SMS ou por e-mail."),

                    _guideService.Create("Se eu tenho mais de uma conta, posso incluir todas no PIX?", "​Você poderá realizar o registro das suas chaves por meio de um dos canais de acesso da instituição em que você possui conta (inclusive aplicativo instalado em seu smartphone). \n\n" + "Para realizar o registro, você precisará confirmar a posse da chave e vinculá-la a uma conta para recebimento dos recursos. Para confirmação da posse da chave, sua instituição enviará um código por SMS para o número de telefone celular que você quer utilizar como chave (ou para o e-mail que se quer utilizar como chave, se for o caso). \n\n" + "Esse código deverá ser inserido no canal de acesso disponibilizado por sua instituição financeira ou de pagamento, mediante autenticação digital apropriada, como solicitação de senha, biometria ou reconhecimento facial, por exemplo. \n\n" + "Atenção! A confirmação não pode ser efetivada por contato telefônico nem por link enviado por meio de SMS ou por e-mail."),

                    _guideService.Create("Posso vincular todas as chaves à mesma conta?", "Sim. Você pode vincular todas as suas chaves (CPF, número de celular e e-mail) a uma mesma conta. \n\n" + "Dessa forma, quando o pagador iniciar o pagamento a partir de qualquer uma dessas informações, os recursos serão disponibilizados nessa mesma conta. \n\n" + "Existe, no entanto, um limite de 5 (cinco) chaves por conta para pessoas físicas e de 20 (vinte) chaves por conta para pessoas jurídicas."),
                };
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Visualizou guia PIX");
            }
        });

        public Command<Guide> OpenAnswerGuideCommand => new Command<Guide>(async (guide) =>
        {
            try
            {
                await DialogService.AlertAsync(guide?.Answer);
            }
            catch (Exception e)
            {
                e.SendToLog();
            }
            finally
            {
                SetEvent("Viu uma resposta do Guia Pix", new Dictionary<string, string> { { "Guia: ", guide?.Answer } });
            }
        });

        private ObservableCollection<Guide> _guideList;
        public ObservableCollection<Guide> GuideList
        {
            set => SetProperty(ref _guideList, value);
            get => _guideList;
        }
    }
}
