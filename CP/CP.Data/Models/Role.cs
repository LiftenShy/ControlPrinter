using System.Collections.Generic;

namespace CP.Data.Models
{
    public class Role : EntityBase
    {
        public string NameRole { get; set; }

        public virtual List<User> Users { get; set; }
    }
}