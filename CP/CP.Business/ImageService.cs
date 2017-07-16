using System;
using System.IO;
using System.Linq;
using CP.Business.Abstract;
using CP.Data;
using CP.Data.Models;
using CP.Storage;

namespace CP.Business
{
    public class ImageService : IImageService
    {
        public ImageService(IRepository<Image> imageRepository, 
            IRepository<StaticImage> staticImgRepository,
            IRepository<ResultImg> resultImgRepository, IFileManager fileManager)
        {
            ImageRepository = imageRepository;
            FileManager = fileManager;
            StaticImgRepository = staticImgRepository;
            ResultImgRepository = resultImgRepository;
        }

        public IFileManager FileManager { get; }
        public IRepository<Image> ImageRepository { get; }
        public IRepository<StaticImage> StaticImgRepository { get; }
        public IRepository<ResultImg> ResultImgRepository { get; }

        public Image GetImage()
        {
            return ImageRepository.Table.OrderByDescending(o => o.DateLoad).FirstOrDefault();
        }

        public StaticImage GetStaticImage()
        {
            return StaticImgRepository.Table.OrderByDescending(o => o.DateLoad).FirstOrDefault();
        }

        public ResultImg GetResultImage()
        {
            return ResultImgRepository.Table.OrderByDescending(o => o.DateLoad).FirstOrDefault();
        }

        public string GetImageUri(string fileName)
        {
            return FileManager.GetUri(fileName);
        }

        public void SaveImage(Image image,Stream imageStream)
        {
            string fileName = Guid.NewGuid().ToString();
            FileManager.SaveFile(fileName, imageStream);
            image.NameImage = fileName;
            ImageRepository.Insert(image);
        }

        public void SaveStatisticImage(StaticImage image, Stream imageStream)
        {
            string fileName = Guid.NewGuid().ToString();
            FileManager.SaveFile(fileName, imageStream);
            image.NameImage = fileName;
            StaticImgRepository.Insert(image);
        }

        public void SaveResultImage(ResultImg image, Stream imageStream)
        {
            string fileName = Guid.NewGuid().ToString();
            FileManager.SaveFile(fileName, imageStream);
            image.NameImage = fileName;
            ResultImgRepository.Insert(image);
        }

        public byte[] GetFile(string fileName)
        {
            return FileManager.GetFile(fileName);
        }

        public void Dispose()
        {
            if (ImageRepository != null)
            {
                ImageRepository.Dispose();
            }
        }
    }
}
