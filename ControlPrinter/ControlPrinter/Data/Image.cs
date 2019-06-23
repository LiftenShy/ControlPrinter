using System;
using System.ComponentModel.DataAnnotations;

namespace ControlPrinter.Data
{
    public class Image
    {
        [Key]
        public long Id { get; set; }

        public string OriginalImagePath { get; set; }

        public string ProcessedImagePath { get; set; }

        public string ResultImagePath { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
