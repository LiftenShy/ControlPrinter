using System;
using Microsoft.AspNetCore.Identity;

namespace ControlPrinter.Models
{
    public class UserModel : IdentityUser
    {
        public string OriginalImageName { get; set; }

        public string ProcessedImage { get; set; }

        public string DifferentBetweenImageName { get; set; }
    }
}
