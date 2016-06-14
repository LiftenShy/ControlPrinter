using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.Data;
using CP.Data.Models;

namespace CP.Business
{
    class RoleService
    {
        
        public bool IsUserInRole(string username, string roleName)
        {
            using (IRepository<Role> roleRepository = new EfRepository<Role>())
            {
                
            }
            return true;
        }
    }
}
