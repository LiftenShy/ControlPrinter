using System.Linq;
using CP.Data;
using CP.Data.Models;

namespace CP.Business
{
    public class UserService : IUserService
    {
        public UserService(IRepository<User> userRepository)
        {
            this.UserRepository = userRepository;
        }

        private IRepository<User> UserRepository { get; set; }

        public bool ValidateUser(string userName, string password)
        {
                return this.UserRepository.Table.Any(u => u.UserName == userName && u.Password == password);
        }

        public User GetUser(string userName)
        {
                return this.UserRepository.Table.FirstOrDefault(f => f.UserName == userName);
        }

        public bool AddUser(User user)
        {
            if (!this.UserRepository.Table.Any(u => u.UserName == user.UserName))
            {
                User item = new User {UserName = user.UserName, Password = user.Password, RoleId = 1};
                this.UserRepository.Insert(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (this.UserRepository != null)
            {
                this.UserRepository.Dispose();
            }
        }
    }
}
