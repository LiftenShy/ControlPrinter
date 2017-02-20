using CP.Data.Models;

namespace CP.Business
{
    public interface IUserService : IServiceBase
    {
        bool ValidateUser(string userName, string password);
        User GetUser(string userName);
        bool AddUser(User user);
    }
}