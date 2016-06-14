using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.Storage
{
    class LocalFileManager : IFileManager
    {
        public void SaveFile(String fileName, Byte[] content)
        {
            throw new NotImplementedException();
        }

        public void SaveFile(String fileName, Stream content)
        {
            throw new NotImplementedException();
        }

        public Byte[] GetFile(String fileName)
        {
            throw new NotImplementedException();
        }

        public String GetURI(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
