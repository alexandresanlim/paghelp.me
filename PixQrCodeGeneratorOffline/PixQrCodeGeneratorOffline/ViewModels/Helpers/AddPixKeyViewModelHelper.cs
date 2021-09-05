using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Models.Base;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels.Helpers
{
    class AddPixKeyViewModelHelper
    {
    }

    public class AddPixInput : NotifyObjectBase
    {
        public string Title { get; set; }

        public string Value { get; set; }

        private string _placeholder;
        public string Placeholder
        {
            set => SetProperty(ref _placeholder, value);
            get => _placeholder;
        }

        public string Icon { get; set; }

        public AddPixInputType Type { get; set; }

        public ReturnType ReturnType { get; set; }

        public bool IsInstitution => Type == AddPixInputType.Institution;

        public static ObservableCollection<AddPixInput> GetList(bool isContact = false)
        {
            var listReturn = new ObservableCollection<AddPixInput>();

            if (!isContact)
            {
                listReturn.Add(new AddPixInput
                {
                    Type = AddPixInputType.Institution,
                    Title = "Instituição / Banco",
                    Icon = FontAwesomeSolid.University,
                    Placeholder = "Selecione a Instituição",
                    ReturnType = ReturnType.Next,
                });
            }

            listReturn.Add(new AddPixInput
            {
                Type = AddPixInputType.Key,
                Title = "Chave",
                Icon = FontAwesomeSolid.Key,
                Placeholder = "Digite a chave",
                ReturnType = ReturnType.Next,
            });

            listReturn.Add(new AddPixInput
            {
                Type = AddPixInputType.Name,
                Title = "Nome",
                Icon = FontAwesomeSolid.User,
                Placeholder = "Digite o nome",
                ReturnType = ReturnType.Next,
            });

            listReturn.Add(new AddPixInput
            {
                Type = AddPixInputType.City,
                Title = "Cidade",
                Icon = FontAwesomeSolid.MapMarkedAlt,
                Placeholder = "Digite a cidade (não obrigatório)",
                ReturnType = ReturnType.Done,
            });

            return listReturn;
        }
    }

    public class InputValues
    {
        public InputValues(ObservableCollection<AddPixInput> inputList)
        {
            Institution = new InputValueInstitution(inputList, inputList?.FirstOrDefault(x => x.Type == AddPixInputType.Institution));
            Key = new InputValueKey(inputList, inputList?.FirstOrDefault(x => x.Type == AddPixInputType.Key));
            Name = new InputValueName(inputList, inputList?.FirstOrDefault(x => x.Type == AddPixInputType.Name));
            City = new InputValueCity(inputList, inputList?.FirstOrDefault(x => x.Type == AddPixInputType.City));
        }

        public InputValueInstitution Institution { get; }

        public InputValueKey Key { get; }

        public InputValueName Name { get; }

        public InputValueCity City { get; }
    }

    public class InputValueBase
    {
        public InputValueBase(ObservableCollection<AddPixInput> inputList, AddPixInput input)
        {
            Input = input;
            Value = input?.Value;
            Index = inputList?.IndexOf(Input) ?? -1;
        }

        public AddPixInput Input { get; set; }

        public string Value { get; set; }

        public int Index { get; set; }
    }

    public class InputValueName : InputValueBase
    {
        public InputValueName(ObservableCollection<AddPixInput> inputList, AddPixInput input) : base(inputList, input) { }
    }

    public class InputValueCity : InputValueBase
    {
        public InputValueCity(ObservableCollection<AddPixInput> inputList, AddPixInput input) : base(inputList, input) { }
    }

    public class InputValueKey : InputValueBase
    {
        public InputValueKey(ObservableCollection<AddPixInput> inputList, AddPixInput input) : base(inputList, input) { }
    }

    public class InputValueInstitution : InputValueBase
    {
        public InputValueInstitution(ObservableCollection<AddPixInput> inputList, AddPixInput input) : base(inputList, input) { }
    }

    public enum AddPixInputType
    {
        Institution,
        Key,
        Name,
        City,
    }
}
