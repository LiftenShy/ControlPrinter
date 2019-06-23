using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlPrinter.Data;
using ControlPrinter.Service.Abstract;
using MessageBus.Models;

namespace ControlPrinter.Service.Implement
{
    public class StorageService : IStorageService
    {
        private readonly IEfRepository<Image> _imageRepository;

        public StorageService(IEfRepository<Image> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public void SavePictures(Image pictures)
        {
            _imageRepository.Add(pictures);
        }

        public Image LoadPicture()
        {
            return _imageRepository.Table.OrderByDescending(x => x.CreateDate).FirstOrDefault();
        }
    }
}
