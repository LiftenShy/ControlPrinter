using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class Role : EntityBase
    {
        public string NameRole { get; set; }

        public virtual List<User> Users { get; set; }
    }
}