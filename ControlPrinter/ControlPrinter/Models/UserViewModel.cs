using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPrinter.Models
{
    public class UserViewModel
    {
        public string OriginalImageName { get; set; }

        public string ProcessedImage { get; set; }

        public string DifferentBetweenImageName { get; set; }
    }
}
