using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.Data;
using CP.Data.Models;

namespace CP.Business
{
    public class UserService : IUserService
    {
        public bool ValidateUser(string userName, string password)
        {
            using (IRepository<User> useRepository = new EfRepository<User>())
            {
                return useRepository.Table.Any(u => u.UserName == userName && u.Password == password);
            }
        }

        public User GetUser(string userName)
        {
            using (IRepository<User> useRepository = new EfRepository<User>())
            {
                return useRepository.Table.FirstOrDefault(f => f.UserName == userName);
            }
        }
    }
}
