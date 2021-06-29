using PixQrCodeGeneratorOffline.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IPixKeyService
    {
        bool IsValid(PixKey pixKey);

        List<PixKey> GetAll();

        PixKey GetById(int id);

        bool Update(PixKey item);

        bool Insert(PixKey item);

        bool Remove(PixKey item);

        void ShareAllKeys();

        Task<bool> RemoveAll();

        Task NavigateToEdit(PixKey pixKey);

        Task NavigateToAdd();

        //Task NavigateToAction(PixKey pixKey);
    }
}
