using System;
using System.IO;
using System.Runtime.InteropServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CP.Storage
{
    public class CloudinaryFileManager : IFileManager
    {

        private static Account _account = new Account("hhgsh0gzm", "773373475616428", "WqZqAgJI6kPykUdTRWwK_7ECrs8");
        private Cloudinary _cloudinary = new Cloudinary(_account);

        public void SaveFile(String fileName, Byte[] content)
        {
            throw new NotImplementedException();
        }

        public void SaveFile(String fileName, Stream content)
        {
            ImageUploadParams uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, content),
                PublicId = fileName
            };
            ImageUploadResult uploadResult = this._cloudinary.Upload(uploadParams);
        }

        public Byte[] GetFile(String fileName)
        {
            throw new NotImplementedException();
        }

        public String GetURI(string fileName)
        {
            return this._cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(400).Height(400).Crop("fill")).BuildUrl(fileName);
        }
    }
}
