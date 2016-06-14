using CP.Data.Models;

namespace CP.Business
{
    public interface IUserService
    {
        bool ValidateUser(string userName, string password);
        User GetUser(string userName);
    }
}