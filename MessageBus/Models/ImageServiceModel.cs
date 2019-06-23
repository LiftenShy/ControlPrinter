using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBus.Models
{
    [Serializable]
    public class ImageServiceModel
    {
        public string OriginalImagePath { get; set; }

        public string ProcessedImagePath { get; set; }

        public string ResultImagePath { get; set; }

        public override string ToString()
        {
            return $"OriginalImagePath: {OriginalImagePath} /n ProcessedImagePath: {ProcessedImagePath} /n ResultImagePath: {ResultImagePath}";
        }
    }
}
