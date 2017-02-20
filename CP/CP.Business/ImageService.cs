using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.Data;
using CP.Data.Models;
using CP.Storage;

namespace CP.Business
{
    public class ImageService : IImageService
    {
        public ImageService(IRepository<Image> imageRepository,IFileManager fileManager)
        {
            this.ImageRepository = imageRepository;
            this.FileManager = fileManager;
        }

        private IFileManager FileManager { get; set; }
        private IRepository<Image> ImageRepository { get; set; } 

        public Image GetImage()
        {
            return this.ImageRepository.Table.OrderByDescending(o => o.DateLoad).FirstOrDefault();
        }

        public string GetImageUri(string fileName)
        {
            return this.FileManager.GetURI(fileName);
        }

        public void SaveImage(Image image,Stream imageStream)
        {
            string fileName = Guid.NewGuid().ToString();
            this.FileManager.SaveFile(fileName, imageStream);
            image.NameImage = fileName;
            this.ImageRepository.Insert(image);
        }

        public void Dispose()
        {
            if (this.ImageRepository != null)
            {
                this.ImageRepository.Dispose();
            }
        }
    }
}
