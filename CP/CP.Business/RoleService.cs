using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.Data;
using CP.Data.Models;

namespace CP.Business
{
    public class RoleService : IRoleService
    {

        public bool IsUserInRole(string username, string roleName)
        {
            using (IRepository<User> userRepository = new EfRepository<User>())
            {
                return userRepository.Table.Any(u => u.UserName == username && u.Role.NameRole == roleName);
            }
        }

        public bool RoleExists(string roleName)
        {
            using (IRepository<Role> roleRepository = new EfRepository<Role>())
            {
                return roleRepository.Table.Any(r => r.NameRole == roleName);
            }
        }

        public string[] GetRolesForUser(string userName)
        {
            using (IRepository<User> userRepository = new EfRepository<User>())
            {
                User user = userRepository.Table.First(u => u.UserName == userName);
                return (new string[] {user.Role.NameRole});
            }
        }
    }
}
