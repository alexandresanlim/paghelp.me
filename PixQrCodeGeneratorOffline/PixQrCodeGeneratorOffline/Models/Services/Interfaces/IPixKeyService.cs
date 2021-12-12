using PixQrCodeGeneratorOffline.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IPixKeyService
    {
        bool IsValid(PixKey pixKey);

        List<PixKey> GetAll(bool isContact = false);

        PixKey GetById(int id);

        bool Update(PixKey item);

        bool Insert(PixKey item);

        bool Remove(PixKey item);

        Task ShareAllKeys(ObservableCollection<PixKey> pixkeyList);

        Task<bool> RemoveAll(bool isContact = false);

        Task NavigateToEdit(PixKey pixKey, bool isContact = false);

        Task NavigateToAdd(bool isContact = false);

        //Task NavigateToAction(PixKey pixKey);
    }
}
