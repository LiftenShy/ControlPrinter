using System;
using System.IO;

namespace CP.Storage
{
    public class LocalFileManager : IFileManager
    {
        private String _basePath;

        public LocalFileManager(String basePath)
        {
            this._basePath = basePath;
        }

        public void SaveFile(String fileName, Byte[] content)
        {
            File.WriteAllBytes(Path.Combine(this._basePath, fileName), content);
        }

        public void SaveFile(String fileName, Stream content)
        {
            using (FileStream fileStream = new FileStream(Path.Combine(this._basePath, fileName), FileMode.Create))
            {
                content.CopyTo(fileStream);
            }
        }

        public Byte[] GetFile(String fileName)
        {
            return File.ReadAllBytes(Path.Combine(this._basePath, fileName));
        }

        public String GetUri(string fileName)
        {
            return Path.Combine(this._basePath, fileName);
        }
    }
}
