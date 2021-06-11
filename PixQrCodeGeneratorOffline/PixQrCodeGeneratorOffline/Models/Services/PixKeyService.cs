using PixQrCodeGeneratorOffline.Models.Repository.Interfaces;
using PixQrCodeGeneratorOffline.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models.Services
{
    public class PixKeyService : IPixKeyService
    {
        private readonly IPixKeyRepository _pixKeyRepository;

        public PixKeyService()
        {
            _pixKeyRepository = DependencyService.Get<IPixKeyRepository>();
        }

        public bool IsValid(PixKey pixKey)
        {
            return !string.IsNullOrWhiteSpace(pixKey?.Key);
        }

        public List<PixKey> GetAll()
        {
            return _pixKeyRepository.GetAll();
        }

        public PixKey GetFirst()
        {
            return _pixKeyRepository.GetFirst();
        }

        public bool Update(PixKey item)
        {
            return _pixKeyRepository.Update(item);
        }

        public bool Insert(PixKey item)
        {
            return _pixKeyRepository.Insert(item);
        }

        public bool Remove(PixKey item)
        {
            return _pixKeyRepository.Remove(item);
        }

        public bool RemoveAll()
        {
            return _pixKeyRepository.RemoveAll();
        }
    }
}
