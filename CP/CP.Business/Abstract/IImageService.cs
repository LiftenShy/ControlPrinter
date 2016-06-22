using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.Data;
using CP.Data.Models;

namespace CP.Business
{
    public interface IImageService : IServiceBase
    {
        Image GetImage();
        void SaveImage(Image image,Stream streamImage);
        string GetImageUri(string fileName);
    }
}
