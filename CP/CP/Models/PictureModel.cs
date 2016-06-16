using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CP.Web.Models
{
    [JsonObject]
    public class PictureModel
    {
        public string FileName { get; set; }
        public FileInfo File { get; set; }

        public PictureModel(string fileName, FileInfo file)
        {
            this.FileName = fileName;
            this.File = file;
        }
    }
}