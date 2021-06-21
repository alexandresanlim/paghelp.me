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

        PixKey GetFirst();

        bool Update(PixKey item);

        bool Insert(PixKey item);

        bool Remove(PixKey item);

        void ShareAllKeys();

        Task<bool> RemoveAll();

        Task NavigateToEdit(DashboardViewModel dashboardVM, PixKey pixKey);

        Task NavigateToAdd(DashboardViewModel dashboardVM);
    }
}
