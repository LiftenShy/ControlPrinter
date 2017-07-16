using System;
using System.Web.Security;
using CP.Business;
using CP.Business.Abstract;
using Microsoft.Practices.Unity;

namespace CP.Security
{
    public class CustomRoleProvider : RoleProvider
    {
        [Dependency]
        public IRoleService RoleService { get; set; }

        public override string[] GetRolesForUser(string userName)
        {
            return this.RoleService.GetRolesForUser(userName);
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return this.RoleService.IsUserInRole(username, roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return this.RoleService.RoleExists(roleName);
        }

        ~CustomRoleProvider()
        {
            if (this.RoleService != null)
            {
                this.RoleService.Dispose();
            }
        }
    }
}