
using System.Drawing;

namespace CP.CSImageService.Service.Abstract
{
    public interface IImageProcessesService
    {
        Bitmap SetBinaryImage(Bitmap image, double threshold);
        Bitmap SobelFilter(Bitmap image);
        Bitmap CompareImages(Bitmap firstImage, Bitmap secondImage);
    }
}
