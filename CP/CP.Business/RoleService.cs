
using System.Linq;
using CP.Business.Abstract;
using CP.Data;
using CP.Data.Models;

namespace CP.Business
{
    public class RoleService : IRoleService
    {
        public RoleService(IRepository<User> userRepository, IRepository<Role> roleRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }

        private IRepository<Role> RoleRepository { get; }
        private IRepository<User> UserRepository { get; }

        public bool IsUserInRole(string username, string roleName)
        {
            return UserRepository.Table.Any(u => u.UserName == username && u.Role.NameRole == roleName);
        }



        public bool RoleExists(string roleName)
        {
            return RoleRepository.Table.Any(r => r.NameRole == roleName);
        }

        public string[] GetRolesForUser(string userName)
        {
            User user = UserRepository.Table.First(u => u.UserName == userName);
            return (new [] {user.Role.NameRole});
        }

        public void Dispose()
        {
            RoleRepository?.Dispose();
            UserRepository?.Dispose();
        }
    }
}
