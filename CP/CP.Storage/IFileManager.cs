using System;
using System.IO;

namespace CP.Storage
{
    public interface IFileManager
    {
        void SaveFile(String fileName, Byte[] content);
        void SaveFile(String fileName, Stream content);
        Byte[] GetFile(String fileName);
        String GetURI(string fileName);
    }
}