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
        List<PixKey> GetAll(bool isContact = false);

        List<PixKey> GetAll(Expression<Func<PixKey, bool>> predicate);

        PixKey GetById(int id);

        bool Update(PixKey item);

        bool Insert(PixKey item);

        bool Remove(PixKey item);

        Task NavigateToShareAllKeys(ObservableCollection<PixKey> pixkeyList);

        void ShareAllKeys(string info);

        Task<bool> RemoveAll(bool isContact = false);

        Task NavigateToAdd(bool isContact = false);

        Task ExportToFile(IList<PixKey> pixkeyList);

        Task ExportToFileContact(IList<PixKey> contactPixkeyList);

        //Task NavigateToAction(PixKey pixKey);
    }
}
