using System.IO;
using CP.Data.Models;

namespace CP.Business.Abstract
{
    public interface IImageService : IServiceBase
    {
        Image GetImage();
        StaticImage GetStaticImage();
        ResultImg GetResultImage();
        void SaveImage(Image image,Stream streamImage);
        void SaveStatisticImage(StaticImage image, Stream imageStream);
        void SaveResultImage(ResultImg image, Stream imageStream);
        string GetImageUri(string fileName);
        byte[] GetFile(string fileName);
    }
}
