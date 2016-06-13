using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CP.Models
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string email)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool isUserInRole = false;
            using (var db = new ControlPrinterDbContext())
            {
                var role =
                    db.Users
                        .Where(u => u.UserName == username && u.Role.NameRole == roleName);

                if (!role.Equals(null))
                {
                    isUserInRole = true;
                }
            }
            return isUserInRole;
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
            bool isRoleExits = false;
            using (var db = new ControlPrinterDbContext())
            {
                var role =
                    db.Roles
                        .Where(r => r.NameRole == roleName);

                if (!role.Equals(null))
                {
                    isRoleExits = true;
                }
            }
            return isRoleExits;
        }
    }
}