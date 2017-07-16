using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.Data.ModelTransport
{
    public class TransportImageModel
    {
        public byte[] ImageBytes { get; set; }
        public Stream Stream { get; set; }
    }
}
