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
        public RoleService(IRepository<User> userRepository, IRepository<Role> roleRepository)
        {
            this.UserRepository = userRepository;
            this.RoleRepository = roleRepository;
        }

        private IRepository<Role> RoleRepository { get; set; }
        private IRepository<User> UserRepository { get; set; }

        public bool IsUserInRole(string username, string roleName)
        {
            return this.UserRepository.Table.Any(u => u.UserName == username && u.Role.NameRole == roleName);
        }

        public bool RoleExists(string roleName)
        {
            return this.RoleRepository.Table.Any(r => r.NameRole == roleName);
        }

        public string[] GetRolesForUser(string userName)
        {
            User user = this.UserRepository.Table.First(u => u.UserName == userName);
            return (new string[] {user.Role.NameRole});
        }

        public void Dispose()
        {
            if (this.RoleRepository != null)
            {
                this.RoleRepository.Dispose();
            }
            if (this.UserRepository != null)
            {
                this.UserRepository.Dispose();
            }
        }
    }
}
