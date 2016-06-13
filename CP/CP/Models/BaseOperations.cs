using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public static class BaseOperations
    {
        public static void AddUserInDb(User objUser)
        {
            using (ControlPrinterDbContext db = new ControlPrinterDbContext())
            {
                User user = new User {UserName = objUser.UserName, Password = objUser.Password, RoleId = 1};
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}