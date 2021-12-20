using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Models.Services.Interfaces
{
    public interface IPixKeyService
    {
        bool IsValid(PixKey pixKey);

        List<PixKey> GetAll(bool isContact = false);

        List<PixKey> GetAll(Expression<Func<PixKey, bool>> predicate);

        PixKey GetById(int id);

        bool Update(PixKey item);

        bool Insert(PixKey item);

        bool Remove(PixKey item);

        Task NavigateToShareAllKeys(ObservableCollection<PixKey> pixkeyList);

        Task ShareAllKeys(string info);

        Task<bool> RemoveAll(bool isContact = false);

        Task NavigateToEdit(PixKey pixKey, bool isContact = false);

        Task NavigateToAdd(bool isContact = false);

        //Task NavigateToAction(PixKey pixKey);
    }
}
