using System;
using System.Configuration;
using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CP.Storage
{
    public class CloudinaryFileManager : IFileManager
    {
        private static Account _account = new Account(
            ConfigurationManager.AppSettings["CloudinaryDotNet.cloud"], 
            ConfigurationManager.AppSettings["CloudinaryDotNet.apiKey"], 
            ConfigurationManager.AppSettings["CloudinaryDotNet.apiSecret"]);
        private readonly Cloudinary _cloudinary = new Cloudinary(_account);

        public void SaveFile(String fileName, Byte[] content)
        {//MemoryStream
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
