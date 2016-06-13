using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CP.Models;

namespace CP.Controllers.Account
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginPost(User user)
        {
            if(Membership.ValidateUser(user.UserName, user.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(user.UserName, true);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                return this.View("Login");
            }
        }

        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return this.View("Login");
        }
    }
}