using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class User : EntityBase
    {
        public  string UserName { get; set; }
        public  string Password { get; set; }

        public  int RoleId { get; set; }
        public virtual  Role Role { get; set; }
    }
}