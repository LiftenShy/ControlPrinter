using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class Image : EntityBase
    {
        public string NameImage { get; set; }
        public DateTime DateLoad { get; set; }
    }
}