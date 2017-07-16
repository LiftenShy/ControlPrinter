
using CP.Data.Models;

namespace CP.Business.Abstract
{
    public interface IUserService : IServiceBase
    {
        bool ValidateUser(string userName, string password);
        User GetUser(string userName);
        bool AddUser(User user);
    }
}