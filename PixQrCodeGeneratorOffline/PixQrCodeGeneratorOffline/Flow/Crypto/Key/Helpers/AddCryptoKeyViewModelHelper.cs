using PixQrCodeGeneratorOffline.Extention;
using PixQrCodeGeneratorOffline.Helpers.Icon;
using PixQrCodeGeneratorOffline.Models.Base;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.ViewModels.Helpers
{
    class AddCryptoKeyViewModelHelper
    {
    }

    public class AddCryptoInput : NotifyObjectBase
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

        public AddCryptoInputType Type { get; set; }

        public ReturnType ReturnType { get; set; }

        public bool IsInstitution => Type == AddCryptoInputType.Institution;

        public static ObservableCollection<AddCryptoInput> GetList(bool isContact = false)
        {
            var listReturn = new ObservableCollection<AddCryptoInput>();

            if (!isContact)
            {
                listReturn.Add(new AddCryptoInput
                {
                    Type = AddCryptoInputType.Institution,
                    Title = "Moeda",
                    Icon = FontAwesomeSolid.University,
                    Placeholder = "Selecione uma moeda",
                    ReturnType = ReturnType.Next,
                });
            }

            listReturn.Add(new AddCryptoInput
            {
                Type = AddCryptoInputType.Key,
                Title = "Chave",
                Icon = FontAwesomeSolid.Key,
                Placeholder = "Digite a chave pública",
                ReturnType = ReturnType.Next,
            });

            return listReturn;
        }
    }

    public class InputCryptoValues
    {
        public InputCryptoValues(ObservableCollection<AddCryptoInput> inputList)
        {
            Institution = new InputCryptoValueInstitution(inputList, inputList?.FirstOrDefault(x => x.Type == AddCryptoInputType.Institution));
            Key = new InputCryptoValueKey(inputList, inputList?.FirstOrDefault(x => x.Type == AddCryptoInputType.Key));
        }

        public InputCryptoValueInstitution Institution { get; }

        public InputCryptoValueKey Key { get; }
    }

    public class InputCryptoValueBase
    {
        public InputCryptoValueBase(ObservableCollection<AddCryptoInput> inputList, AddCryptoInput input)
        {
            Input = input;
            Value = input?.Value;
            Index = inputList?.IndexOf(Input) ?? -1;
        }

        public AddCryptoInput Input { get; set; }

        public string Value { get; set; }

        public int Index { get; set; }
    }

    public class InputCryptoValueKey : InputCryptoValueBase
    {
        public InputCryptoValueKey(ObservableCollection<AddCryptoInput> inputList, AddCryptoInput input) : base(inputList, input) { }
    }

    public class InputCryptoValueInstitution : InputCryptoValueBase
    {
        public InputCryptoValueInstitution(ObservableCollection<AddCryptoInput> inputList, AddCryptoInput input) : base(inputList, input) { }
    }

    public enum AddCryptoInputType
    {
        Institution,
        Key
    }
}
