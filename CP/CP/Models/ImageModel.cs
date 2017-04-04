using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace CP.Web.Models
{
    public class ImageModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }


        public IntPtr Image { get; set; }
        public Image<Gray, Byte>[] Images { get; set; }
    }
}