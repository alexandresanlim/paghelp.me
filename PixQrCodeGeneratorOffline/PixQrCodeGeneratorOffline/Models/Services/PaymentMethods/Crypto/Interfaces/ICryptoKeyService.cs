using PixQrCodeGeneratorOffline.Models.PaymentMethods.Crypto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PixQrCodeGeneratorOffline.Models.Services.PaymentMethods.Crypto.Interfaces
{
    public interface ICryptoKeyService
    {
        bool IsValid(CryptoKey key);

        List<CryptoKey> GetAll(bool isContact = false);

        List<CryptoKey> GetAll(Expression<Func<CryptoKey, bool>> predicate);

        CryptoKey GetById(int id);

        bool Update(CryptoKey item);

        bool Insert(CryptoKey item);

        bool Remove(CryptoKey item);

        Task NavigateToShareAllKeys(ObservableCollection<CryptoKey> pixkeyList);

        Task ShareAllKeys(string info);

        Task<bool> RemoveAll(bool isContact = false);

        Task NavigateToEdit(CryptoKey pixKey, bool isContact = false);

        Task NavigateToAdd(bool isContact = false);
    }
}
