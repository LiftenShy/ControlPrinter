using System;
using System.IO;

namespace CP.Storage
{
    public interface IFileManager
    {
        void SaveFile(String filePath, Byte[] content);
        void SaveFile(String filePath, Stream content);
        Byte[] GetFile(String fileName);
        String GetURI(string fileName);
    }
}