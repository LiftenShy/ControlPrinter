using System;
using System.Linq;
using ControlPrinter.Data;
using ControlPrinter.Models;
using ControlPrinter.Service.Abstract;

namespace ControlPrinter.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IEfRepository<UserModel> _userRepository;

        public UserService(IEfRepository<UserModel> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel GetByName(string userName)
        {
            return _userRepository.Table.SingleOrDefault(x => x.UserName.Equals(userName));
        }
    }
}
