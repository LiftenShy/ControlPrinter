
namespace CP.Business.Abstract
{
    public interface IRoleService : IServiceBase
    {
        bool IsUserInRole(string username, string roleName);
        bool RoleExists(string roleName);
        string[] GetRolesForUser(string userName);
    }
}