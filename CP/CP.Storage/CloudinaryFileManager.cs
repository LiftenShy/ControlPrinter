using System;
using System.Configuration;
using System.IO;
using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CP.Storage
{
    public class CloudinaryFileManager : IFileManager
    {
        private static readonly Account _account = new Account(
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
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, content),
                PublicId = fileName
            };
            ImageUploadResult uploadResult = this._cloudinary.Upload(uploadParams);
        }

        public Byte[] GetFile(String fileName)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadData(new Uri(GetUri(fileName)));
            }
        }

        public String GetUri(string fileName)
        {
            return _cloudinary.Api.UrlImgUp.BuildUrl(fileName);
            //Transform(new Transformation().Width(400).Height(400).Crop("fill"))
        }
    }
}
